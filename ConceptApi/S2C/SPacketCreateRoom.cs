using System.Runtime.InteropServices;
using ConceptApi.C2S;

namespace ConceptApi.S2C
{
	public readonly struct SPacketCreateRoom
	{
		public readonly CreateRoomStatus Status;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
		public readonly string RoomId;

		public SPacketCreateRoom(CreateRoomStatus status, string roomId)
		{
			Status = status;
			RoomId = roomId;
		}
	}
}