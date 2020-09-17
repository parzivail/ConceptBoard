using System;
using System.Drawing;
using ConceptApi.C2S;
using Lidgren.Network;

namespace ConceptApi.Board
{
	public class GamePiece : INetworkSerializable
	{
		public Guid Id { get; private set; }
		public GamePieceType Type { get; private set; }

		public float X { get; private set; }
		public float Y { get; private set; }

		public GamePiece()
		{
		}

		public GamePiece(Guid id, GamePieceType type, float x, float y)
		{
			Id = id;
			Type = type;
			X = x;
			Y = y;
		}

		/// <inheritdoc />
		public void WriteToBuffer(NetBuffer buffer)
		{
			buffer.Write(Id.ToByteArray());
			buffer.Write((byte)Type);
			buffer.Write(X);
			buffer.Write(Y);
		}

		/// <inheritdoc />
		public void ReadFromBuffer(NetBuffer buffer)
		{
			Id = new Guid(buffer.ReadBytes(16));
			Type = (GamePieceType) buffer.ReadByte();
			X = buffer.ReadFloat();
			Y = buffer.ReadFloat();
		}
	}
}
