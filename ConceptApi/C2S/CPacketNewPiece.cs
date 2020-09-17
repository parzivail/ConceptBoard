using System;
using ConceptApi.Board;

namespace ConceptApi.C2S
{
	public readonly struct CPacketNewPiece
	{
		public readonly GamePieceType Type;
		public readonly Guid Id;
		public readonly float X;
		public readonly float Y;

		public CPacketNewPiece(GamePieceType type, Guid id, float x, float y)
		{
			Type = type;
			Id = id;
			X = x;
			Y = y;
		}
	}
}