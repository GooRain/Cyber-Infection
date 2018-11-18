using System.Linq;
using Generation.Room;
using UnityEngine;

namespace Generation.Map
{
	public class Map
	{
		public RoomType[,] roomMatrix;
		
		public int width { get; }
		public int height { get; }

		public Map(int width, int height)
		{
			this.width = width;
			this.height = height;
			Debug.Log($"{width} / {height}");
			roomMatrix = new RoomType[this.width, this.height];
		}
		
		public bool HasEnd()
		{
			return roomMatrix.Cast<RoomType>().Any(roomType => (roomType & RoomType.Boss) != 0);
		}

		public void Clear()
		{
			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					roomMatrix[x, y] = RoomType.None;
				}
			}
		}
	}
}