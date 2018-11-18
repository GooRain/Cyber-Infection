using System;
using Extension;
using Generation.Room;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Map
{
	public class GeneratingEntity
	{
		private enum EntityMoveDirection
		{
			Right = 0,
			Up = 1,
			Left = 2,
			Down = 3
		}

		private readonly Map _map;
		private PointInt _currentPosition;

		public GeneratingEntity(ref Map map, PointInt currentPosition)
		{
			_map = map;
			_currentPosition = currentPosition;
		}

		public void Move()
		{
			var moveDirection = (EntityMoveDirection) Random.Range(0, 4);

			var offset = GetOffset(moveDirection);
			_currentPosition.x = Mathf.Clamp(_currentPosition.x + offset.x, 0, _map.width);
			_currentPosition.y = Mathf.Clamp(_currentPosition.y + offset.y, 0, _map.height);
		}

		public void PlaceRoom(RoomType room)
		{
			_map.roomMatrix[_currentPosition.x, _currentPosition.y] = room;
		}

		public bool CanPlace()
		{
			return (_map.roomMatrix[_currentPosition.x, _currentPosition.y] | RoomType.None) == 0;
		}

		private PointInt GetOffset(EntityMoveDirection direction)
		{
			switch (direction)
			{
				case EntityMoveDirection.Right:
					return new PointInt(1, 0);
				case EntityMoveDirection.Up:
					return new PointInt(0, 1);
				case EntityMoveDirection.Left:
					return new PointInt(-1, 0);
				case EntityMoveDirection.Down:
					return new PointInt(0, -1);
				default:
					return new PointInt(0, 0);
			}
		}
	}
}