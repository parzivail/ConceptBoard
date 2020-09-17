using System;
using System.Collections.Generic;
using System.Text;
using ConceptApi.Board;

namespace ConceptApi.C2S
{
	public readonly struct CPacketFloatingPiece
	{
		public readonly GamePieceType Type;
		public readonly float X;
		public readonly float Y;

		public CPacketFloatingPiece(GamePieceType type, float x, float y)
		{
			Type = type;
			X = x;
			Y = y;
		}
	}
}
