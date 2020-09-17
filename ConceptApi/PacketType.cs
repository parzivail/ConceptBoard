using System.Text;

namespace ConceptApi
{
	public enum PacketType : short
	{
		CPacketJoinRoom,
		SPacketJoinRoom,
		CPacketCreateRoom,
		SPacketCreateRoom,
		SPacketSyncRoom,
		CPacketClientControl,
		CPacketFloatingPiece,
		SPacketFloatingPiece,
		CPacketNewPiece,
		CPacketDestroyPiece
	}
}
