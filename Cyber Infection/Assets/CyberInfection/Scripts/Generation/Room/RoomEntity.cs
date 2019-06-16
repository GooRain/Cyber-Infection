using System.Collections.Generic;
using CyberInfection.Generation.Tiles;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
	public class RoomEntity
	{
		private readonly RoomTemplate template;
		private readonly DoorDirDoorsDictionary doorsDictionary;
		private readonly RoomType type;
		
		public int Width => template.width;
		public int Height => template.height;

		public DoorDirDoorsDictionary DoorsDictionary => doorsDictionary;
		public RoomTemplate Template => template;

		public RoomType Type => type;

		public RoomEntity(RoomType type)
		{
			this.type = type;
			doorsDictionary = new DoorDirDoorsDictionary();
		}

		public RoomEntity(RoomTemplate template, RoomType type)
		{
			this.template = template;
			this.type = type;
			doorsDictionary = new DoorDirDoorsDictionary();
			InitializeDoors();
		}

		private void InitializeDoors()
		{
			for (var x = 0; x < template.width; x++)
			{
				TryToAddDoor(DoorDir.Top, x, template.height - 1);
				TryToAddDoor(DoorDir.Bottom, x, 0);
			}

			for (var y = 0; y < template.height; y++)
			{
				TryToAddDoor(DoorDir.Left, 0, y);
				TryToAddDoor(DoorDir.Right, template.width - 1, y);
			}
		}

		private void TryToAddDoor(DoorDir dir, int x, int y)
		{
			if (IsDoorTileAt(x, y))
			{
				AddDoor(dir, x, y);
			}
		}

		private void AddDoor(DoorDir dir, int x, int y)
		{
			if (!doorsDictionary.ContainsKey(dir))
			{
				doorsDictionary.Add(dir, new List<Vector3Int>());
			}
			
			doorsDictionary[dir].Add(new Vector3Int(x, y, 0));
		}

		private bool IsDoorTileAt(int x, int y)
		{
			return IsDoor(template[x, y]);
		}

		private bool IsDoor(TileType tileType)
		{
			return tileType == TileType.Door;
		}
	}
}