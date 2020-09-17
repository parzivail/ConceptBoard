using System;

namespace ConceptApi.Board
{
	[Flags]
	public enum GamePieceType : byte
	{
		LightBulb = 1,
		QuestionMark = 2,
		Exclamation = 4,
		Cube = 8,
		Red = 16,
		Yellow = 32,
		Green = 64,
		Blue = 128
	}
}