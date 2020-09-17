using ConceptApi.C2S;

namespace ConceptApi.S2C
{
	public readonly struct SPacketJoinRoom
	{
		public readonly JoinRoomStatus Status;

		public SPacketJoinRoom(JoinRoomStatus status)
		{
			Status = status;
		}
	}
}