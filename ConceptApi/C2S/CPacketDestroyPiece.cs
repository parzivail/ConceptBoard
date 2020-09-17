using System;

namespace ConceptApi.C2S
{
	public readonly struct CPacketDestroyPiece
	{
		public readonly Guid Id;

		public CPacketDestroyPiece(Guid id)
		{
			Id = id;
		}
	}
}