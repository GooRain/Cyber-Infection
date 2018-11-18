using System;
using System.Collections.Generic;
using Extension;
using Generation.Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Room
{
	[Flags]
	public enum Doors
	{
		None = 0,
		Right = 1,
		Up = 2,
		Left = 4,
		Down = 8,
		
		All = 15
	}
	public class RoomController : MonoBehaviour
	{
		public Room room { get; set; }
		
		public List<WallTile> Walls { get; set; }

		private void Awake()
		{
			Walls = new List<WallTile>();
		}
	}
}
