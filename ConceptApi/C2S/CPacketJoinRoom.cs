using System.Runtime.InteropServices;

namespace ConceptApi.C2S
{
	public readonly struct CPacketJoinRoom
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
		public readonly string RoomId;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
		public readonly string RoomKey;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
		public readonly string Nickname;

		public CPacketJoinRoom(string roomId, string roomKey, string nickname)
		{
			RoomId = roomId;
			RoomKey = roomKey;
			Nickname = nickname;
		}
	}
}
