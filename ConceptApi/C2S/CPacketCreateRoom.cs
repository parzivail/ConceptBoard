using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ConceptApi.Board;
using Lidgren.Network;

namespace ConceptApi.C2S
{
	public readonly struct CPacketCreateRoom
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
		public readonly string RoomKey;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
		public readonly string Nickname;

		public CPacketCreateRoom(string roomKey, string nickname)
		{
			RoomKey = roomKey;
			Nickname = nickname;
		}
	}

	public class SPacketSyncRoom : INetworkSerializable
	{
		public string RoomId { get; private set; }
		public Guid CurrentPlayer { get; private set; }
		public Player[] Players { get; private set; }
		public GamePiece[] Board { get; private set; }
		public int YourPlayer { get; private set; }

		public SPacketSyncRoom()
		{
		}

		public SPacketSyncRoom(string roomId, int yourPlayer, Guid currentPlayer, Player[] players, GamePiece[] board)
		{
			RoomId = roomId;
			YourPlayer = yourPlayer;
			CurrentPlayer = currentPlayer;
			Players = players;
			Board = board;
		}

		/// <inheritdoc />
		public void WriteToBuffer(NetBuffer buffer)
		{
			buffer.Write(RoomId);
			buffer.Write(YourPlayer);
			buffer.Write(CurrentPlayer.ToByteArray());

			buffer.Write(Players.Length);
			foreach (var player in Players) player.WriteToBuffer(buffer);

			buffer.Write(Board.Length);
			foreach (var piece in Board) piece.WriteToBuffer(buffer);
		}

		/// <inheritdoc />
		public void ReadFromBuffer(NetBuffer buffer)
		{
			RoomId = buffer.ReadString();
			YourPlayer = buffer.ReadInt32();
			CurrentPlayer = new Guid(buffer.ReadBytes(16));

			Players = new Player[buffer.ReadInt32()];
			for (var i = 0; i < Players.Length; i++) Players[i] = buffer.ReadClass<Player>();

			Board = new GamePiece[buffer.ReadInt32()];
			for (var i = 0; i < Board.Length; i++) Board[i] = buffer.ReadClass<GamePiece>();
		}
	}

	public interface INetworkSerializable
	{
		void WriteToBuffer(NetBuffer buffer);
		void ReadFromBuffer(NetBuffer buffer);
	}
}
