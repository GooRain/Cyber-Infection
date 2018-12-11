using System;
using System.Collections.Generic;
using Extension;
using Generation.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;
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

		private List<Tile> _myTiles = new List<Tile>();

		public void TryToToggle(bool value)
		{
			if (value)
			{
				// ...
				Activate();
				// ...
			}
			else
			{
				// ...
				Deactivate();
				// ...
			}
		}

		private void Deactivate()
		{
			var transparent = Color.white;
			transparent.a = 0f;
			foreach (var tile in _myTiles)
			{
				tile.color = transparent;
			}
		}

		public void Activate()
		{
			var transparent = Color.white;
			transparent.a = 1f;
			foreach (var tile in _myTiles)
			{
				tile.color = transparent;
			}
		}

	}
}
