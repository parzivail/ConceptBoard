using System;
using System.Collections.Generic;
using ConceptApi.C2S;
using ConceptApi.S2C;

namespace ConceptApi
{
	public static class PacketTypeMap
	{
		public static Dictionary<Type, PacketType> Map = new Dictionary<Type, PacketType>
		{
			{ typeof(CPacketJoinRoom), PacketType.CPacketJoinRoom },
			{ typeof(SPacketJoinRoom), PacketType.SPacketJoinRoom },
			{ typeof(CPacketCreateRoom), PacketType.CPacketCreateRoom },
			{ typeof(SPacketCreateRoom), PacketType.SPacketCreateRoom },
			{ typeof(SPacketSyncRoom), PacketType.SPacketSyncRoom },
			{ typeof(CPacketClientControl), PacketType.CPacketClientControl },
			{ typeof(CPacketFloatingPiece), PacketType.CPacketFloatingPiece },
			{ typeof(SPacketFloatingPiece), PacketType.SPacketFloatingPiece },
			{ typeof(CPacketNewPiece), PacketType.CPacketNewPiece },
			{ typeof(CPacketDestroyPiece), PacketType.CPacketDestroyPiece }
		};
	}
}