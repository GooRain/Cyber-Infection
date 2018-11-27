using Boo.Lang;
using Data.Settings.Generation;
using Generation.Room;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Generation.Map
{
	public class MapController : MonoBehaviour
	{
		public Map map;

		private List<RoomController> _roomControllers = new List<RoomController>();
		private MapSettingsData _mapSettingsData;

		public void Initialize(MapSettingsData data)
		{
			_mapSettingsData = data;
			map = new Map(data.mapSize.width, data.mapSize.height);
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
			for (var x = 0; x < _mapSettingsData.mapSize.width; x++)
			{
				for (var y = 0; y < _mapSettingsData.mapSize.height; y++)
				{
					var currentPosition = new Vector3Int(
						x * _mapSettingsData.roomSizeInfo.roomWidth,
						y * _mapSettingsData.roomSizeInfo.roomHeight,
						0
					);

					if ((map.roomMatrix[x, y] | RoomType.None) == 0) continue;

					SetFloor(floorTileMap, currentPosition, _mapSettingsData.roomSizeInfo);
					SetWall(wallTileMap, currentPosition, _mapSettingsData.roomSizeInfo);
				}
			}
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