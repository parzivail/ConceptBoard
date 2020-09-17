using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using ConceptApi.C2S;
using Lidgren.Network;

namespace ConceptApi
{
	public static class NetBufferExtensions
	{
		public static void WriteStruct<T>(this NetBuffer buffer, T str) where T : struct
		{
			var size = Marshal.SizeOf(str);

			if (size == 0)
				return;

			var arr = new byte[size];

			var ptr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(str, ptr, true);
			Marshal.Copy(ptr, arr, 0, size);
			Marshal.FreeHGlobal(ptr);

			buffer.Write(arr);
		}

		public static T ReadStruct<T>(this NetBuffer buffer) where T : struct
		{
			var str = new T();

			var size = Marshal.SizeOf(str);

			if (size == 0)
				return str;

			var ptr = Marshal.AllocHGlobal(size);

			var arr = buffer.ReadBytes(size);
			Marshal.Copy(arr, 0, ptr, size);

			str = (T)Marshal.PtrToStructure(ptr, str.GetType());
			Marshal.FreeHGlobal(ptr);

			return str;
		}

		public static T ReadClass<T>(this NetBuffer buffer) where T : INetworkSerializable, new()
		{
			var t = new T();
			t.ReadFromBuffer(buffer);
			return t;
		}

		public static NetOutgoingMessage CreateClassMessage<T>(this NetServer server, T str) where T : INetworkSerializable
		{
			var msg = server.CreateMessage();
			msg.Write((short)PacketTypeMap.Map[str.GetType()]);
			str.WriteToBuffer(msg);
			return msg;
		}

		public static NetOutgoingMessage CreateStructMessage<T>(this NetServer server, T str) where T : struct
		{
			var msg = server.CreateMessage(Marshal.SizeOf(str));
			msg.Write((short)PacketTypeMap.Map[str.GetType()]);
			msg.WriteStruct(str);
			return msg;
		}

		public static NetOutgoingMessage CreateStructMessage<T>(this NetClient server, T str) where T : struct
		{
			var msg = server.CreateMessage(Marshal.SizeOf(str));
			msg.Write((short)PacketTypeMap.Map[str.GetType()]);
			msg.WriteStruct(str);
			return msg;
		}
	}
}
