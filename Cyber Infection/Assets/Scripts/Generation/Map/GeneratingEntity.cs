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
			None = 0,
			Right = 1,
			Up = 2,
			Left = 3,
			Down = 4
		}

		private readonly Map _map;
		private PointInt _currentPosition;
		private EntityMoveDirection _previousDirection = EntityMoveDirection.None;

		private int _debugID;

		public GeneratingEntity(ref Map map, PointInt currentPosition)
		{
			_map = map;
			_currentPosition = currentPosition;
			_debugID = Guid.NewGuid().GetHashCode() % 1000;
		}

		public void Move()
		{
			var moveDirection = (EntityMoveDirection) Random.Range(1, 5);
			while (moveDirection == GetInvertedDirection(_previousDirection))
			{
				moveDirection = (EntityMoveDirection) Random.Range(1, 5);
			}
			
			Debug.Log($"[{_debugID}] moving {moveDirection}");

			var offset = GetOffset(moveDirection);
			_currentPosition.x = Mathf.Clamp(_currentPosition.x + offset.x, 0, _map.width - 1);
			_currentPosition.y = Mathf.Clamp(_currentPosition.y + offset.y, 0, _map.height - 1);

			_previousDirection = moveDirection;
		}

		public void PlaceRoom(RoomType room)
		{
			_map.roomMatrix[_currentPosition.x, _currentPosition.y] = room;
			Debug.Log($"Placing {room} at [{_currentPosition.x},{_currentPosition.y}]");
		}

		public bool CanPlace()
		{
			return (_map.roomMatrix[_currentPosition.x, _currentPosition.y] | RoomType.None) == 0;
		}

		private EntityMoveDirection GetInvertedDirection(EntityMoveDirection direction)
		{
			switch(direction)
			{
				case EntityMoveDirection.None:
					return EntityMoveDirection.None;
				case EntityMoveDirection.Right:
					return EntityMoveDirection.Left;
				case EntityMoveDirection.Up:
					return EntityMoveDirection.Down;
				case EntityMoveDirection.Left:
					return EntityMoveDirection.Right;
				case EntityMoveDirection.Down:
					return EntityMoveDirection.Up;
				default:
					Debug.LogError(direction);
					return EntityMoveDirection.None;
			}
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