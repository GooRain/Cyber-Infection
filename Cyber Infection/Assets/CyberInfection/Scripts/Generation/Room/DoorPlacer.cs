using System.Collections.Generic;
using CyberInfection.Data.Settings.Generation;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CyberInfection.Generation.Room
{
    public class DoorPlacer
    {
        private readonly Map _map;
        private readonly MapSettingsData _mapSettingsData;
        private readonly Transform _mapHolder;
        private readonly RoomController[,] _roomControllersMatrix;

        private readonly Dictionary<string, bool> _doorsIDs = new Dictionary<string, bool>();

        public DoorPlacer(Map map, MapSettingsData mapSettingsData, Transform mapHolder
            , RoomController[,] roomControllersMatrix)
        {
            _map = map;
            _mapSettingsData = mapSettingsData;
            _mapHolder = mapHolder;
            _roomControllersMatrix = roomControllersMatrix;
        }

        public void SetDoors(MapTilemaps tilemaps, int x, int y, Vector3Int center, RoomSizeInfo roomSizeInfo,
            RoomType[,] mapRoomMatrix)
        {
            if (x > 0 && mapRoomMatrix[x - 1, y] != RoomType.None)
            {
                SetDoor(tilemaps, x - 1, y, x, y,
                    center - new Vector3Int(roomSizeInfo.roomWidth / 2, 0, 0), DoorController.DoorType.Horizontal);
            }

            if (x < _map.width - 1 && mapRoomMatrix[x + 1, y] != RoomType.None)
            {
                SetDoor(tilemaps, x, y, x + 1, y,
                    center + new Vector3Int(roomSizeInfo.roomWidth / 2, 0, 0), DoorController.DoorType.Horizontal);
            }

            if (y > 0 && mapRoomMatrix[x, y - 1] != RoomType.None)
            {
                SetDoor(tilemaps, x, y - 1, x, y,
                    center - new Vector3Int(0, roomSizeInfo.roomHeight / 2, 0), DoorController.DoorType.Vertical);
            }

            if (y < _map.height - 1 && mapRoomMatrix[x, y + 1] != RoomType.None)
            {
                SetDoor(tilemaps, x, y, x, y + 1,
                    center + new Vector3Int(0, roomSizeInfo.roomHeight / 2, 0), DoorController.DoorType.Vertical);
            }
        }

        private void SetDoor(MapTilemaps tilemaps,
            int x1, int y1, int x2, int y2, Vector3Int pos, DoorController.DoorType type)
        {
            tilemaps.WallTilemap.SetTile(pos, null);
            tilemaps.ShadowTilemap.SetTile(pos, null);

            PlaceDoor(tilemaps, type, pos, x1, y1, x2, y2);
        }

        private void PlaceDoor(MapTilemaps tilemaps, DoorController.DoorType doorType, Vector3Int pos,
            int x1, int y1, int x2, int y2)
        {
            if (HasDoor(x1, y1, x2, y2))
            {
                return;
            }

            //tilemaps.FloorTilemap.SetTile(pos, _mapSettingsData.GetDoorTile());

//            var doorGo = new GameObject($"HorizontalDoor[{GetDoorID(x1, y1, x2, y2)}]");
//            doorGo.transform.SetParent(_mapHolder);
//            doorGo.transform.localPosition = pos + Vector3.one * 0.5f;
//            var door = doorGo.AddComponent<DoorController>();
//            var doorTrigger = doorGo.AddComponent<BoxCollider2D>();
//            doorTrigger.isTrigger = true;
            
            //var doorGO = tilemaps.FloorTilemap.GetInstantiatedObject(pos);
            var doorGO = Object.Instantiate(tilemaps.DoorPrefab, _mapHolder);
            doorGO.name = $"HorizontalDoor[{GetDoorID(x1, y1, x2, y2)}]";
            doorGO.transform.localPosition = pos;
            
            if (doorGO == null)
            {
                Debug.Log(pos + " = DOOR GO IS NULL");
                return;
            }
            
            var door = doorGO.GetComponent<DoorController>();
            if (door == null)
            {
                Debug.Log("DOOR CONTROLLER IS NULL");
                return;
            }

            var firstRoomController = _roomControllersMatrix[x1, y1];
            var secondRoomController = _roomControllersMatrix[x2, y2];
            
            door.Initialize(doorType, firstRoomController, secondRoomController);
            door.Toggle(true);
                
            firstRoomController.FloorTiles.Add(pos);
            secondRoomController.FloorTiles.Add(pos);

            _doorsIDs.Add(GetDoorID(x1, y1, x2, y2), true);
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
    }
}