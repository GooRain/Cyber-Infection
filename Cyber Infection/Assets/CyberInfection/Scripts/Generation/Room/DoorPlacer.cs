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

        public void SetDoors(Tilemap wallTileMap, Tilemap shadowTileMap, Tilemap floorTileMap,
            int x, int y,
            Vector3Int center,
            RoomSizeInfo roomSizeInfo, RoomType[,] mapRoomMatrix)
        {
//            if (x > 0 && mapRoomMatrix[x - 1, y] != RoomType.None)
//            {
//                var leftDoorPos = center - new Vector3Int(roomSizeInfo.roomWidth / 2, 0, 0);
//                wallTileMap.SetTile(leftDoorPos, null);
//                shadowTileMap.SetTile(leftDoorPos, null);
//                floorTileMap.SetTile(leftDoorPos, _mapSettingsData.GetFloorTile());
//                //Debug.Log($"Set Left Door at: {leftDoorPos}");
//
//                PlaceDoor(Door.DoorType.Horizontal, leftDoorPos,
//                    mapRoomMatrix, x - 1, y, x, y);
//            }
            if (x > 0 && mapRoomMatrix[x - 1, y] != RoomType.None)
            {
                SetDoor(wallTileMap, shadowTileMap, floorTileMap, x - 1, y, x, y,
                    center - new Vector3Int(roomSizeInfo.roomWidth / 2, 0, 0), Door.DoorType.Horizontal);
            }

//            if (x < map.width - 1 && mapRoomMatrix[x + 1, y] != RoomType.None)
//            {
//                var rightDoorPos = center + new Vector3Int(roomSizeInfo.roomWidth / 2, 0, 0);
//                wallTileMap.SetTile(rightDoorPos, null);
//                shadowTileMap.SetTile(rightDoorPos, null);
//                floorTileMap.SetTile(rightDoorPos, _mapSettingsData.GetFloorTile());
//                //Debug.Log($"Set Right Door at: {rightDoorPos}");
//
//                PlaceDoor(Door.DoorType.Horizontal, rightDoorPos,
//                    mapRoomMatrix, x, y, x + 1, y);
//            }

            if (x < _map.width - 1 && mapRoomMatrix[x + 1, y] != RoomType.None)
            {
                SetDoor(wallTileMap, shadowTileMap, floorTileMap, x, y, x + 1, y,
                    center + new Vector3Int(roomSizeInfo.roomWidth / 2, 0, 0), Door.DoorType.Horizontal);
            }

//            if (y > 0 && mapRoomMatrix[x, y - 1] != RoomType.None)
//            {
//                var downDoorPos = center - new Vector3Int(0, roomSizeInfo.roomHeight / 2, 0);
//                wallTileMap.SetTile(downDoorPos, null);
//                shadowTileMap.SetTile(downDoorPos, null);
//                floorTileMap.SetTile(downDoorPos, _mapSettingsData.GetFloorTile());
//                //Debug.Log($"Set Down Door at: {downDoorPos}");
//                PlaceDoor(Door.DoorType.Vertical, downDoorPos,
//                    mapRoomMatrix, x, y - 1, x, y);
//            }

            if (y > 0 && mapRoomMatrix[x, y - 1] != RoomType.None)
            {
                SetDoor(wallTileMap, shadowTileMap, floorTileMap, x, y - 1, x, y,
                    center - new Vector3Int(0, roomSizeInfo.roomHeight / 2, 0), Door.DoorType.Vertical);
            }

//            if (y < map.height - 1 && mapRoomMatrix[x, y + 1] != RoomType.None)
//            {
//                var upDoorPos = center + new Vector3Int(0, roomSizeInfo.roomHeight / 2, 0);
//                wallTileMap.SetTile(upDoorPos, null);
//                shadowTileMap.SetTile(upDoorPos, null);
//                floorTileMap.SetTile(upDoorPos, _mapSettingsData.GetFloorTile());
//                //Debug.Log($"Set Up Door at: {upDoorPos}");
//                PlaceDoor(Door.DoorType.Vertical, upDoorPos,
//                    mapRoomMatrix, x, y, x, y + 1);
//            }
            if (y < _map.height - 1 && mapRoomMatrix[x, y + 1] != RoomType.None)
            {
                SetDoor(wallTileMap, shadowTileMap, floorTileMap, x, y, x, y + 1,
                    center + new Vector3Int(0, roomSizeInfo.roomHeight / 2, 0), Door.DoorType.Vertical);
            }
        }

        private void SetDoor(Tilemap wallTileMap, Tilemap shadowTileMap, Tilemap floorTileMap,
            int x1, int y1, int x2, int y2, Vector3Int pos, Door.DoorType type)
        {
            wallTileMap.SetTile(pos, null);
            shadowTileMap.SetTile(pos, null);

            PlaceDoor(floorTileMap, type, pos, x1, y1, x2, y2);
        }

        private void PlaceDoor(Tilemap floorTilemap, Door.DoorType doorType, Vector3Int pos,
            int x1, int y1, int x2, int y2)
        {
            if (HasDoor(x1, y1, x2, y2))
            {
                return;
            }

            floorTilemap.SetTile(pos, _mapSettingsData.GetFloorTile());

            var doorGo = new GameObject($"HorizontalDoor[{GetDoorID(x1, y1, x2, y2)}]");
            doorGo.transform.SetParent(_mapHolder);
            doorGo.transform.localPosition = pos + Vector3.one * 0.5f;
            var door = doorGo.AddComponent<Door>();
            var doorTrigger = doorGo.AddComponent<BoxCollider2D>();
            doorTrigger.isTrigger = true;

            var firstRoomController = _roomControllersMatrix[x1, y1];
            var secondRoomController = _roomControllersMatrix[x2, y2];

            door.Initialize(doorType, firstRoomController, secondRoomController);
            
            firstRoomController.floorTiles.Add(pos);
            secondRoomController.floorTiles.Add(pos);

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