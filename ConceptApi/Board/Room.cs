using System;
using System.Collections.Generic;
using System.Text;

namespace ConceptApi.Board
{
	public class Room
	{
		public readonly string Id;
		public readonly List<Player> Players;
		public readonly List<GamePiece> Board;

		public Guid CurrentPlayer { get; set; }

		public Room(string id)
		{
			Id = id;

			Players = new List<Player>();
			Board = new List<GamePiece>();
		}
	}
}
