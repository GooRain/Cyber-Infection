using System.Collections.Generic;
using CyberInfection.Data.Settings.Generation;
using CyberInfection.Generation.Room;
using CyberInfection.Generation.Room.Door;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CyberInfection.Generation.Map
{
	public class MapController : MonoBehaviour
	{
		public Map map;

		private List<RoomController> _roomControllers = new List<RoomController>();
		[SerializeField] private RoomController[,] _roomControllersMatrix;
		private MapSettingsData _mapSettingsData;
		private Dictionary<string, bool> _doorsIDs = new Dictionary<string, bool>();

		private Vector3 _offset;
		private Transform _mapHolder;

		public void Initialize(MapSettingsData data, Vector3 offset, Transform mapHolder)
		{
			_mapSettingsData = data;
			_offset = offset;
			_mapHolder = mapHolder;
			
			map = new Map(data.mapSize.width, data.mapSize.height);
			_roomControllersMatrix = new RoomController[data.mapSize.width, data.mapSize.height];
		}

		public void Clear()
		{
			foreach (var roomController in _roomControllers)
			{
				Destroy(roomController.gameObject);
			}

			map.Clear();
		}

		public void PlaceRooms(Tilemap floorTileMap, Tilemap wallTileMap)
		{
			for (var x = 0; x < map.width; x++)
			{
				for (var y = 0; y < map.height; y++)
				{
					var currentPosition = new Vector3Int(
						x * (_mapSettingsData.roomSizeInfo.roomWidth - 1),
						y * (_mapSettingsData.roomSizeInfo.roomHeight - 1),
						0
					);

					if ((map.roomMatrix[x, y] | RoomType.None) == 0) continue;

					SetFloor(floorTileMap, currentPosition, _mapSettingsData.roomSizeInfo);
					SetWall(wallTileMap, currentPosition, _mapSettingsData.roomSizeInfo);
				}
			}

			SetRoomControllers();

			PlaceDoors(floorTileMap, wallTileMap);
		}

		private void PlaceDoors(Tilemap floorTileMap, Tilemap wallTileMap)
		{
			for (var x = 0; x < map.width; x++)
			{
				for (var y = 0; y < map.height; y++)
				{
					var currentPosition = new Vector3Int(
						x * (_mapSettingsData.roomSizeInfo.roomWidth - 1),
						y * (_mapSettingsData.roomSizeInfo.roomHeight - 1),
						0
					);

					if ((map.roomMatrix[x, y] | RoomType.None) == 0) continue;

					SetDoors(wallTileMap, floorTileMap, map.roomMatrix, x, y, currentPosition,
						_mapSettingsData.roomSizeInfo);
				}
			}
		}

		private void SetRoomControllers()
		{
			var roomIndex = 0;
			var roomControllersHolder = new GameObject("RoomControllers Holder");
			roomControllersHolder.transform.SetParent(_mapHolder);
			
			for (var x = 0; x < map.width; x++)
			{
				for (var y = 0; y < map.height; y++)
				{
					var currentPosition = _offset + new Vector3Int(
						                      x * (_mapSettingsData.roomSizeInfo.roomWidth - 1),
						                      y * (_mapSettingsData.roomSizeInfo.roomHeight - 1),
						                      0
					                      );
					
					if ((map.roomMatrix[x, y] | RoomType.None) == 0) continue;
					
					var roomGameObject = new GameObject($"RoomController#{roomIndex++}");
					var newRoomController = roomGameObject.AddComponent<RoomController>();
					var roomControllerTransform = newRoomController.transform;
					roomControllerTransform.SetParent(roomControllersHolder.transform);
					roomControllerTransform.localPosition = currentPosition + Vector3.one * 0.5f;
					_roomControllers.Add(newRoomController);
					_roomControllersMatrix[x, y] = newRoomController;
				}
			}
		}

		//	TODO Vitcor: Надо будет получше сделать
		private void SetDoors(Tilemap wallTileMap, Tilemap floorTileMap, RoomType[,] mapRoomMatrix, int x, int y,
			Vector3Int center,
			RoomSizeInfo roomSizeInfo)
		{
			if (x > 0 && mapRoomMatrix[x - 1, y] != RoomType.None)
			{
				var leftDoorPos = center - new Vector3Int(roomSizeInfo.roomWidth / 2, 0, 0);
				wallTileMap.SetTile(leftDoorPos, null);
				floorTileMap.SetTile(leftDoorPos, _mapSettingsData.GetFloorTile());
				//Debug.Log($"Set Left Door at: {leftDoorPos}");

				PlaceDoor(Door.DoorType.Horizontal, leftDoorPos,
					mapRoomMatrix, x - 1, y, x, y);
			}

			if (x < map.width - 1 && mapRoomMatrix[x + 1, y] != RoomType.None)
			{
				var rightDoorPos = center + new Vector3Int(roomSizeInfo.roomWidth / 2, 0, 0);
				wallTileMap.SetTile(rightDoorPos, null);
				floorTileMap.SetTile(rightDoorPos, _mapSettingsData.GetFloorTile());
				//Debug.Log($"Set Right Door at: {rightDoorPos}");

				PlaceDoor(Door.DoorType.Horizontal, rightDoorPos,
					mapRoomMatrix, x, y, x + 1, y);
			}

			if (y > 0 && mapRoomMatrix[x, y - 1] != RoomType.None)
			{
				var downDoorPos = center - new Vector3Int(0, roomSizeInfo.roomHeight / 2, 0);
				wallTileMap.SetTile(downDoorPos, null);
				floorTileMap.SetTile(downDoorPos, _mapSettingsData.GetFloorTile());
				//Debug.Log($"Set Down Door at: {downDoorPos}");
				PlaceDoor(Door.DoorType.Vertical, downDoorPos,
					mapRoomMatrix, x, y - 1, x, y);
			}

			if (y < map.height - 1 && mapRoomMatrix[x, y + 1] != RoomType.None)
			{
				var upDoorPos = center + new Vector3Int(0, roomSizeInfo.roomHeight / 2, 0);
				wallTileMap.SetTile(upDoorPos, null);
				floorTileMap.SetTile(upDoorPos, _mapSettingsData.GetFloorTile());
				//Debug.Log($"Set Up Door at: {upDoorPos}");
				PlaceDoor(Door.DoorType.Vertical, upDoorPos,
					mapRoomMatrix, x, y, x, y + 1);
			}
		}

		private void PlaceDoor(Door.DoorType doorType, Vector3 pos, RoomType[,] mapRoomMatrix, int x, int y, int x2,
			int y2)
		{
			if (HasDoor(x, y, x2, y2))
			{
				return;
			}

			var doorGo = new GameObject($"HorizontalDoor[{GetDoorID(x, y, x2, y2)}]");
			doorGo.transform.SetParent(_mapHolder);
			doorGo.transform.localPosition = pos + Vector3.one * 0.5f;
			var door = doorGo.AddComponent<Door>();
			var doorTrigger = doorGo.AddComponent<BoxCollider2D>();
			doorTrigger.isTrigger = true;

			door.Initialize(doorType, _roomControllersMatrix[x, y], _roomControllersMatrix[x2, y2]);

			_doorsIDs.Add(GetDoorID(x, y, x2, y2), true);
		}

		private bool HasDoor(int x1, int y1, int x2, int y2)
		{
			return _doorsIDs.ContainsKey(GetDoorID(x1, y1, x2, y2)) ||
			       _doorsIDs.ContainsKey(GetDoorID(x2, y2, x1, y1));
		}

		private string GetDoorID(int x1, int y1, int x2, int y2)
		{
			return $"{x1}{y1} / {x2}{y2}";
		}

		private void SetWall(Tilemap tileMap, Vector3Int center, RoomSizeInfo roomSizeInfo)
		{
			var start = new Vector3Int(center.x - roomSizeInfo.roomWidth / 2, center.y - roomSizeInfo.roomHeight / 2,
				center.z);
			for (var x = 0; x < roomSizeInfo.roomWidth; x++)
			{
				tileMap.SetTile(start + new Vector3Int(x, 0, 0), _mapSettingsData.GetWallTile());
				tileMap.SetTile(start + new Vector3Int(x, roomSizeInfo.roomHeight - 1, 0),
					_mapSettingsData.GetWallTile());
			}

			for (var y = 0; y < roomSizeInfo.roomHeight; y++)
			{
				tileMap.SetTile(start + new Vector3Int(0, y, 0), _mapSettingsData.GetWallTile());
				tileMap.SetTile(start + new Vector3Int(roomSizeInfo.roomWidth - 1, y, 0),
					_mapSettingsData.GetWallTile());
			}
		}

		private void SetFloor(Tilemap tileMap, Vector3Int center, RoomSizeInfo roomSizeInfo)
		{
			var start = new Vector3Int(center.x - roomSizeInfo.roomWidth / 2, center.y - roomSizeInfo.roomHeight / 2,
				center.z);
			for (var x = 1; x < roomSizeInfo.roomWidth - 1; x++)
			{
				for (var y = 1; y < roomSizeInfo.roomHeight - 1; y++)
				{
					//Debug.Log("Set floor tile at: " + (start + new Vector3Int(x, y, 0)));
					tileMap.SetTile(start + new Vector3Int(x, y, 0), _mapSettingsData.GetFloorTile());
				}
			}
		}
	}
}