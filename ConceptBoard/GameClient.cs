using System;
using System.Collections.Generic;
using System.Threading;
using ConceptApi;
using ConceptApi.Board;
using ConceptApi.C2S;
using ConceptApi.S2C;
using Lidgren.Network;

namespace ConceptBoard
{
	public sealed class GameClient
	{
		private static readonly NetClient Client;
		
		public static event EventHandler<EventArgs> Connected;
		public static event EventHandler<EventArgs> Disconnected;

		public static event EventHandler<EventArgs> TurnStarted;

		public static event EventHandler<SPacketJoinRoom> JoinRoomResponse;
		public static event EventHandler<SPacketCreateRoom> CreateRoomResponse;
		public static event EventHandler<SPacketSyncRoom> SyncRoom;
		public static event EventHandler<SPacketFloatingPiece> FloatingPiece;

		public static Room Room;
		public static Player Player;

		static GameClient()
		{
			var config = new NetPeerConfiguration("concept")
			{
				AutoFlushSendQueue = false
			};
			Client = new NetClient(config);

			Client.RegisterReceivedCallback(GotMessage);
			Client.Start();
		}

		public static bool MyTurn => Room.CurrentPlayer == Player.Id;

		private static void GotMessage(object state)
		{
			NetIncomingMessage incomingMessage;
			while ((incomingMessage = Client.ReadMessage()) != null)
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
						Console.WriteLine($"{status}, Reason: {reason}");

						switch (status)
						{
							case NetConnectionStatus.Connected:
								Connected?.Invoke(null, EventArgs.Empty);
								break;
							case NetConnectionStatus.Disconnected:
								Disconnected?.Invoke(null, EventArgs.Empty);
								break;
						}

						break;
					case NetIncomingMessageType.Data:
						var messageType = (PacketType)incomingMessage.ReadInt16();

						switch (messageType)
						{
							case PacketType.SPacketJoinRoom:
								JoinRoomResponse?.Invoke(null, incomingMessage.ReadStruct<SPacketJoinRoom>());
								break;
							case PacketType.SPacketCreateRoom:
								CreateRoomResponse?.Invoke(null, incomingMessage.ReadStruct<SPacketCreateRoom>());
								break;
							case PacketType.SPacketSyncRoom:
							{
								var packet = incomingMessage.ReadClass<SPacketSyncRoom>();

								if (Room == null || Room.Id != packet.RoomId)
									Room = new Room(packet.RoomId);

								Player = packet.Players[packet.YourPlayer];

								Room.Players.Clear();
								Room.Players.AddRange(packet.Players);

								Room.Board.Clear();
								Room.Board.AddRange(packet.Board);

								if (Room.CurrentPlayer != Player.Id && packet.CurrentPlayer == Player.Id) TurnStarted?.Invoke(null, EventArgs.Empty);

								Room.CurrentPlayer = packet.CurrentPlayer;

								SyncRoom?.Invoke(null, packet);
								break;
							}
							case PacketType.SPacketFloatingPiece:
								FloatingPiece?.Invoke(null, incomingMessage.ReadStruct<SPacketFloatingPiece>());
								break;
						}
						break;
					default:
						Console.WriteLine($"Unhandled type: {incomingMessage.MessageType} {incomingMessage.LengthBytes} bytes");
						break;
				}
				Client.FlushSendQueue();
				Client.Recycle(incomingMessage);
			}
		}

		public static bool Connect(string host)
		{
			if (Client.ConnectionStatus == NetConnectionStatus.Connected)
				Disconnect();

			try
			{
				Client.Connect(host, GameInformation.Port);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static void BeginJoinRoom(string roomId, string roomKey, string nickname)
		{
			Client.SendMessage(Client.CreateStructMessage(new CPacketJoinRoom(roomId, roomKey, nickname)), NetDeliveryMethod.ReliableUnordered);
			Client.FlushSendQueue();
		}

		public static void BeginCreateRoom(string roomKey, string nickname)
		{
			Client.SendMessage(Client.CreateStructMessage(new CPacketCreateRoom(roomKey, nickname)), NetDeliveryMethod.ReliableUnordered);
			Client.FlushSendQueue();
		}

		public static void Disconnect()
		{
			Client.Disconnect("Requested by user");
		}

		public static void LeaveRoom()
		{
			Client.SendMessage(Client.CreateStructMessage(new CPacketClientControl(ClientControlFunction.Leave)), NetDeliveryMethod.ReliableUnordered);
			Client.FlushSendQueue();
		}

		public static void NextRound()
		{
			Client.SendMessage(Client.CreateStructMessage(new CPacketClientControl(ClientControlFunction.NextPlayer)), NetDeliveryMethod.ReliableUnordered);
			Client.FlushSendQueue();
		}

		public static void UpdateFloatingPiece(GamePieceType pieceType, float x, float y)
		{
			Client.SendMessage(Client.CreateStructMessage(new CPacketFloatingPiece(pieceType, x, y)), NetDeliveryMethod.UnreliableSequenced);
			Client.FlushSendQueue();
		}

		public static void CreatePiece(GamePieceType pieceType, float x, float y)
		{
			var piece = new GamePiece(Guid.NewGuid(), pieceType, x, y);
			Room.Board.Add(piece);

			Client.SendMessage(Client.CreateStructMessage(new CPacketNewPiece(piece.Type, piece.Id, piece.X, piece.Y)), NetDeliveryMethod.ReliableUnordered);
			Client.FlushSendQueue();
		}

		public static void DestroyPiece(Guid pieceId)
		{
			Client.SendMessage(Client.CreateStructMessage(new CPacketDestroyPiece(pieceId)), NetDeliveryMethod.ReliableUnordered);
			Client.FlushSendQueue();
		}
	}
}
