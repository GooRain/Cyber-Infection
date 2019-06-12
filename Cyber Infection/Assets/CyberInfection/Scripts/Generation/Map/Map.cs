using System.Linq;
using CyberInfection.Generation.Room;
using UnityEngine;

namespace CyberInfection.Generation
{
	public class Map
	{
		public readonly RoomType[,] roomMatrix;
		
		public int Width { get; }
		public int Height { get; }

		public Map(int width, int height)
		{
			this.Width = width;
			this.Height = height;
//			Debug.Log($"{width} / {height}");
			roomMatrix = new RoomType[this.Width, this.Height];
		}
		
		public bool HasEnd()
		{
			return roomMatrix.Cast<RoomType>().Any(roomType => (roomType & RoomType.End) != 0);
		}

		public void Clear()
		{
			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					roomMatrix[x, y] = RoomType.None;
				}
			}
		}

		public RoomType this[int x, int y] => roomMatrix[x, y];
		public RoomType this[Vector2Int pos] => roomMatrix[pos.x, pos.y];
	}
}