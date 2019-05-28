using System.Collections.Generic;
using CyberInfection.Data.Settings.Generation;
using CyberInfection.GameMechanics;
using CyberInfection.Generation.Room;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CyberInfection.Generation
{
    public class MapController : MonoBehaviour
    {
        public Map map;

        private List<RoomController> _roomControllers = new List<RoomController>();
        [SerializeField] private RoomController[,] _roomControllersMatrix;
        private MapSettingsData _mapSettingsData;

        private Vector3 _offset;
        private Transform _mapHolder;
        private DoorPlacer _doorPlacer;

        public List<RoomController> roomControllers => _roomControllers;

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

        public void PlaceRooms(MapTilemaps tilemaps)
        {
            var roomIndex = 0;
            var roomControllersHolder = new GameObject("RoomControllers Holder");
            roomControllersHolder.transform.SetParent(_mapHolder);

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

                    var roomGameObject = new GameObject($"RoomController#{roomIndex++}");
                    var newRoomController = roomGameObject.AddComponent<RoomController>();
                    newRoomController.room = new Room.Room(map.roomMatrix[x, y]);
                    var roomControllerTransform = newRoomController.transform;
                    roomControllerTransform.SetParent(roomControllersHolder.transform);
                    roomControllerTransform.localPosition = currentPosition + _offset + Vector3.one * 0.5f;
                    _roomControllers.Add(newRoomController);
                    _roomControllersMatrix[x, y] = newRoomController;

                    newRoomController.FloorTiles.AddRange(SetFloor(tilemaps, currentPosition, _mapSettingsData.roomSizeInfo));
                    newRoomController.WallTiles.AddRange(SetWalls(tilemaps, currentPosition, _mapSettingsData.roomSizeInfo,
                        _mapSettingsData.GetWallTile()));
                    //SetWall(shadowTileMap, currentPosition, _mapSettingsData.roomSizeInfo, _mapSettingsData.GetShadowTile());
                }
            }

            PlaceDoors(tilemaps);
        }

        private void PlaceDoors(MapTilemaps tilemaps)
        {
            _doorPlacer = new DoorPlacer(map, _mapSettingsData, _mapHolder, _roomControllersMatrix);

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

                    _doorPlacer.SetDoors(tilemaps, x, y, currentPosition,
                        _mapSettingsData.roomSizeInfo, map.roomMatrix);
                }
            }
        }

        private IEnumerable<Vector3Int> SetWalls(MapTilemaps tilemaps, Vector3Int center, RoomSizeInfo roomSizeInfo, TileBase tile)
        {
            var tiles = new List<Vector3Int>();
            var start = new Vector3Int(center.x - roomSizeInfo.roomWidth / 2, center.y - roomSizeInfo.roomHeight / 2,
                center.z);
            for (var x = 0; x < roomSizeInfo.roomWidth; x++)
            {
                var bottom = start + new Vector3Int(x, 0, 0);
                var top = start + new Vector3Int(x, roomSizeInfo.roomHeight - 1, 0);
                
                tilemaps.WallTilemap.SetTile(bottom, tile);
                tilemaps.WallTilemap.SetTile(top, tile);

                tiles.Add(bottom);
                tiles.Add(top);
            }

            for (var y = 0; y < roomSizeInfo.roomHeight; y++)
            {
                var left = start + new Vector3Int(0, y, 0);
                var right = start + new Vector3Int(roomSizeInfo.roomWidth - 1, y, 0);
                
                tilemaps.WallTilemap.SetTile(left, tile);
                tilemaps.WallTilemap.SetTile(right, tile);
                
                tiles.Add(left);
                tiles.Add(right);
            }

            return tiles;
        }

        private IEnumerable<Vector3Int> SetFloor(MapTilemaps tilemaps, Vector3Int center, RoomSizeInfo roomSizeInfo)
        {
            var tiles = new List<Vector3Int>();
            var start = new Vector3Int(center.x - roomSizeInfo.roomWidth / 2, center.y - roomSizeInfo.roomHeight / 2,
                center.z);
            for (var x = 1; x < roomSizeInfo.roomWidth - 1; x++)
            {
                for (var y = 1; y < roomSizeInfo.roomHeight - 1; y++)
                {
                    //Debug.Log("Set floor tile at: " + (start + new Vector3Int(x, y, 0)));
                    var pos = start + new Vector3Int(x, y, 0);
                    var newTile = _mapSettingsData.GetFloorTile();
                    tilemaps.FloorTilemap.SetTile(pos, newTile);
                    tiles.Add(pos);
                }
            }

            return tiles;
        }

        public void OnEndGenerate()
        {
            foreach (var roomController in _roomControllers)
            {
                roomController.TryToToggle(false);
            }
            
            foreach (var roomController in _roomControllers)
            {
                if ((roomController.room.type & RoomType.Start) == RoomType.Start)
                {
                    LevelController.instance.level.SelectRoomController(roomController);
                    break;
                }
            }
        }
    }
}