using ConceptApi.Board;

namespace ConceptApi.S2C
{
	public readonly struct SPacketFloatingPiece
	{
		public GamePieceType Type { get; }
		public float X { get; }
		public float Y { get; }

		public SPacketFloatingPiece(GamePieceType type, float x, float y)
		{
			Type = type;
			X = x;
			Y = y;
		}
	}
}