using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using ConceptApi;
using ConceptApi.Board;
using ConceptApi.C2S;
using ConceptApi.S2C;
using Lidgren.Network;

namespace ConceptServer
{
	class Program
	{
		private static NetServer _server;

		private static readonly Dictionary<string, ServerRoom> Rooms = new Dictionary<string, ServerRoom>();
		private static readonly Dictionary<long, string> PlayerMap = new Dictionary<long, string>();

		static void Main(string[] args)
		{
			var config = new NetPeerConfiguration("concept")
			{
				MaximumConnections = 100,
				Port = GameInformation.Port
			};
			_server = new NetServer(config);

			_server.Start();

			while (true)
			{
				NetIncomingMessage incomingMessage;
				while ((incomingMessage = _server.ReadMessage()) != null)
				{
					// handle incoming message
					switch (incomingMessage.MessageType)
					{
						case NetIncomingMessageType.DebugMessage:
						case NetIncomingMessageType.ErrorMessage:
						case NetIncomingMessageType.WarningMessage:
						case NetIncomingMessageType.VerboseDebugMessage:
							var text = incomingMessage.ReadString();
							Console.WriteLine($"{incomingMessage.MessageType}: {text}");
							break;

						case NetIncomingMessageType.StatusChanged:
							var status = (NetConnectionStatus)incomingMessage.ReadByte();

							var reason = incomingMessage.ReadString();
							Console.WriteLine($"{NetUtility.ToHexString(incomingMessage.SenderConnection.RemoteUniqueIdentifier)} {status}: {reason}");

							if (status == NetConnectionStatus.Connected && incomingMessage.SenderConnection.RemoteHailMessage != null)
								Console.WriteLine($"Remote hail: {incomingMessage.SenderConnection.RemoteHailMessage.ReadString()}");

							if (status == NetConnectionStatus.Disconnected)
								RemoveConnectionFromRoom(incomingMessage.SenderConnection);

							break;
						case NetIncomingMessageType.Data:
							// incoming message from a client
							var messageType = (PacketType)incomingMessage.ReadInt16();

							switch (messageType)
							{
								case PacketType.CPacketJoinRoom:
									OnCPacketJoinRoom(incomingMessage.SenderConnection, incomingMessage.ReadStruct<CPacketJoinRoom>());
									break;
								case PacketType.CPacketCreateRoom:
									OnCPacketCreateRoom(incomingMessage.SenderConnection, incomingMessage.ReadStruct<CPacketCreateRoom>());
									break;
								case PacketType.CPacketClientControl:
									OnCPacketClientControl(incomingMessage.SenderConnection, incomingMessage.ReadStruct<CPacketClientControl>());
									break;
								case PacketType.CPacketFloatingPiece:
									OnCPacketFloatingPiece(incomingMessage.SenderConnection, incomingMessage.ReadStruct<CPacketFloatingPiece>());
									break;
								case PacketType.CPacketNewPiece:
									OnCPacketNewPiece(incomingMessage.SenderConnection, incomingMessage.ReadStruct<CPacketNewPiece>());
									break;
								case PacketType.CPacketDestroyPiece:
									OnCPacketDestroyPiece(incomingMessage.SenderConnection, incomingMessage.ReadStruct<CPacketDestroyPiece>());
									break;
							}
							break;
						default:
							Console.WriteLine($"Unhandled type: {incomingMessage.MessageType} {incomingMessage.LengthBytes} bytes {incomingMessage.DeliveryMethod}|{incomingMessage.SequenceChannel}");
							break;
					}
					_server.Recycle(incomingMessage);
				}
				_server.FlushSendQueue();
				Thread.Sleep(1);
			}
		}

		private static void OnCPacketDestroyPiece(NetConnection connection, CPacketDestroyPiece packet)
		{
			if (!IsConnectionCurrentPlayer(connection))
				return;

			var roomId = PlayerMap[connection.RemoteUniqueIdentifier];
			var room = Rooms[roomId];

			room.Board.RemoveAll(piece => piece.Id == packet.Id);

			SyncRoom(roomId);
		}

		private static void OnCPacketNewPiece(NetConnection connection, CPacketNewPiece packet)
		{
			if (!IsConnectionCurrentPlayer(connection))
				return;

			var roomId = PlayerMap[connection.RemoteUniqueIdentifier];
			var room = Rooms[roomId];

			room.Board.Add(new GamePiece(packet.Id, packet.Type, packet.X, packet.Y));

			SyncRoom(roomId);
		}

		private static void OnCPacketFloatingPiece(NetConnection connection, CPacketFloatingPiece packet)
		{
			if (!IsConnectionCurrentPlayer(connection))
				return;

			var room = Rooms[PlayerMap[connection.RemoteUniqueIdentifier]];

			foreach (var player in room.Players.Where(player => player.RemoteClient.RemoteUniqueIdentifier != connection.RemoteUniqueIdentifier))
				player.RemoteClient.SendMessage(
					_server.CreateStructMessage(new SPacketFloatingPiece(packet.Type, packet.X, packet.Y)),
					NetDeliveryMethod.UnreliableSequenced, 0);
		}

		private static bool IsConnectionCurrentPlayer(NetConnection connection)
		{
			var remoteId = connection.RemoteUniqueIdentifier;
			if (!PlayerMap.ContainsKey(remoteId))
				return false;

			var roomId = PlayerMap[remoteId];
			var room = Rooms[roomId];

			var player = room.Players.FirstOrDefault(serverPlayer =>
				serverPlayer.RemoteClient.RemoteUniqueIdentifier == remoteId);

			return player != null && room.CurrentPlayer == player.Id;
		}

		private static void RemoveConnectionFromRoom(NetConnection connection)
		{
			var remoteId = connection.RemoteUniqueIdentifier;
			if (!PlayerMap.ContainsKey(remoteId))
				return;

			var roomId = PlayerMap[remoteId];
			var room = Rooms[roomId];

			var player = room.Players.FirstOrDefault(serverPlayer =>
				serverPlayer.RemoteClient.RemoteUniqueIdentifier == remoteId);

			if (player == null)
			{
				Console.WriteLine("! Player present in PlayerMap disconnected without being in a room");
				return;
			}

			RemovePlayerFromRoom(roomId, player);
		}

		private static void OnCPacketClientControl(NetConnection connection, CPacketClientControl packet)
		{
			switch (packet.Function)
			{
				case ClientControlFunction.NextPlayer:
				{
					if (!IsConnectionCurrentPlayer(connection))
						break;

					SelectNextPlayer(PlayerMap[connection.RemoteUniqueIdentifier]);
					break;
				}
				case ClientControlFunction.Leave:
					RemoveConnectionFromRoom(connection);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static void OnCPacketJoinRoom(NetConnection connection, CPacketJoinRoom packet)
		{
			var roomId = packet.RoomId.ToUpper();

			if (!Rooms.ContainsKey(roomId))
			{
				connection.SendMessage(_server.CreateStructMessage(new SPacketJoinRoom(JoinRoomStatus.RoomNotFound)),
					NetDeliveryMethod.ReliableUnordered, 0);
				return;
			}

			if (Rooms[roomId].RoomKey != packet.RoomKey)
			{
				connection.SendMessage(_server.CreateStructMessage(new SPacketJoinRoom(JoinRoomStatus.RoomKeyIncorrect)),
					NetDeliveryMethod.ReliableUnordered, 0);
				return;
			}

			connection.SendMessage(_server.CreateStructMessage(new SPacketJoinRoom(JoinRoomStatus.Success)),
				NetDeliveryMethod.ReliableUnordered, 0);

			AddPlayerToRoom(roomId, connection, packet.Nickname);
		}

		private static void OnCPacketCreateRoom(NetConnection connection, CPacketCreateRoom packet)
		{
			using RandomNumberGenerator rng = new RNGCryptoServiceProvider();
			var tokenData = new byte[2];

			string roomId;
			do
			{
				rng.GetBytes(tokenData);
				roomId = AlphaOnlyDataEncoder.Encode(tokenData);
			} while (Rooms.ContainsKey(roomId));

			Rooms[roomId] = new ServerRoom(roomId, packet.RoomKey);
			Console.WriteLine($"* Created room {roomId}");

			connection.SendMessage(_server.CreateStructMessage(new SPacketCreateRoom(CreateRoomStatus.Success, roomId)), NetDeliveryMethod.ReliableUnordered, 0);

			AddPlayerToRoom(roomId, connection, packet.Nickname);

			SelectNextPlayer(roomId);
		}

		private static void RemovePlayerFromRoom(string roomId, ServerPlayer player)
		{
			var room = Rooms[roomId];

			PlayerMap.Remove(player.RemoteClient.RemoteUniqueIdentifier);

			room.Players.Remove(player);
			Console.WriteLine($"* Removed {player} from {roomId}");

			if (room.Players.Count == 0)
			{
				DestroyRoom(roomId);
				return;
			}

			if (room.CurrentPlayer == player.Id)
				SelectNextPlayer(roomId);

			SyncRoom(roomId);
		}

		private static void DestroyRoom(string roomId)
		{
			Rooms.Remove(roomId);
			Console.WriteLine($"* Destroyed room {roomId}");
		}

		private static void SelectNextPlayer(string roomId)
		{
			var room = Rooms[roomId];

			var idx = room.Players.FindIndex(player => player.Id == room.CurrentPlayer);

			idx++;
			idx %= room.Players.Count;

			room.CurrentPlayer = room.Players[idx].Id;

			room.Board.Clear();

			SyncRoom(roomId);
		}

		private static void AddPlayerToRoom(string roomId, NetConnection connection, string nickname)
		{
			var room = Rooms[roomId];

			PlayerMap[connection.RemoteUniqueIdentifier] = roomId;

			var newPlayer = new ServerPlayer(Guid.NewGuid(), nickname, connection);
			room.Players.Add(newPlayer);
			Console.WriteLine($"* Added {newPlayer} to {roomId}");

			SyncRoom(roomId);
		}

		private static void SyncRoom(string roomId)
		{
			var room = Rooms[roomId];
			foreach (var player in room.Players)
			{
				player.RemoteClient.SendMessage(
					_server.CreateClassMessage(
						new SPacketSyncRoom(
							roomId,
							room.Players.FindIndex(serverPlayer => serverPlayer.RemoteClient.RemoteUniqueIdentifier == player.RemoteClient.RemoteUniqueIdentifier),
							room.CurrentPlayer,
							room.Players.Cast<Player>().ToArray(),
							room.Board.ToArray()
							)),
					NetDeliveryMethod.ReliableUnordered, 0);
			}
		}
	}

	internal class ServerRoom
	{
		public readonly string Id;
		public readonly string RoomKey;
		public readonly List<ServerPlayer> Players;
		public readonly List<GamePiece> Board;

		public Guid CurrentPlayer { get; set; }

		public ServerRoom(string id, string roomKey)
		{
			Id = id;
			RoomKey = roomKey;

			Players = new List<ServerPlayer>();
			Board = new List<GamePiece>();
		}
	}

	internal class ServerPlayer : Player
	{
		public readonly NetConnection RemoteClient;

		public ServerPlayer(Guid id, string nickname, NetConnection remoteClient) : base(id, nickname)
		{
			RemoteClient = remoteClient;
		}
	}
}
