using CyberInfection.Generation.Tiles;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
	public class Room
	{
		private readonly RoomTemplate template;
		private readonly DoorDirDoorsDictionary doorsDictionary;
		private readonly RoomType type;

		public DoorDirDoorsDictionary DoorsDictionary => doorsDictionary;
		public RoomTemplate Template => template;

		public RoomType Type => type;

		public Room(RoomTemplate template, RoomType type)
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