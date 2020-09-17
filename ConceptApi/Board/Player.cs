using System;
using ConceptApi.C2S;
using Lidgren.Network;

namespace ConceptApi.Board
{
	public class Player : INetworkSerializable
	{
		public Guid Id { get; private set; }
		public string Nickname { get; private set; }

		public Player()
		{
		}

		public Player(Guid id, string nickname)
		{
			Id = id;
			Nickname = nickname;
		}

		/// <inheritdoc />
		public void WriteToBuffer(NetBuffer buffer)
		{
			buffer.Write(Id.ToByteArray());
			buffer.Write(Nickname);
		}

		/// <inheritdoc />
		public void ReadFromBuffer(NetBuffer buffer)
		{
			Id = new Guid(buffer.ReadBytes(16));
			Nickname = buffer.ReadString();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"\"{Nickname}\" ({Id})";
		}
	}
}