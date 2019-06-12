using CyberInfection.Data.Settings.Generation;
using CyberInfection.Generation.Tiles;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
    public class CorridorCreator
    {
        private const int MIN_LENGTH = 3;

        private readonly MapSettingsData mapSettingsData;
        
        public CorridorCreator(MapSettingsData mapSettingsData)
        {
            this.mapSettingsData = mapSettingsData;
        }

        public Vector3Int GetPathToNextDoor(RoomEntity from, RoomEntity to, MapTilemaps mapTilemaps, DoorDir dir)
        {
            var nextDoorPos = GetNextDoorPos(from, to, mapTilemaps, dir, out var length);
            PlaceCorridor(nextDoorPos, length, mapTilemaps, dir);
            
            return 
        }

        private Vector3Int GetNextDoorPos(RoomEntity from, RoomEntity to, MapTilemaps mapTilemaps, DoorDir dir
        , out int length)
        {
            length = MIN_LENGTH;
            var fromDoor = SelectDoor(dir, from);
            var moveDir = dir.GetMoveDir();
            var offset = moveDir * MIN_LENGTH;
            var pos = fromDoor + offset;
            
            var roomPos = fromDoor

            while (DoCornersHasTile(pos, from.Template.width, from.Template.height, mapTilemaps.TypeTilemap))
            {
                pos += moveDir;
                length++;
            }

            return pos;
        }

        private void PlaceCorridor(Vector3Int from, int length, MapTilemaps mapTilemaps
        , DoorDir dir)
        {
            var moveDir = dir.GetMoveDir();
            var pos = from;

            for (var i = 0; i < length; i++)
            {
                pos += moveDir;
                PlaceBlock(pos, moveDir, mapTilemaps);
            }
        }

        private void PlaceBlock(Vector3Int pos, Vector3Int moveDir, MapTilemaps mapTilemaps)
        {
            mapTilemaps[TileType.Floor].SetTile(pos, mapSettingsData.GetTile(TileType.Floor));
            mapTilemaps[TileType.Wall].SetTile(pos + new Vector3Int(moveDir.y, moveDir.x, moveDir.z), mapSettingsData.GetTile(TileType.Wall));
            mapTilemaps[TileType.Wall].SetTile(pos - new Vector3Int(moveDir.y, moveDir.x, moveDir.z), mapSettingsData.GetTile(TileType.Wall));
        }
        
        private void PlaceHorizontalBlock(Vector3Int pos, MapTilemaps mapTilemaps)
        {
            mapTilemaps[TileType.Floor].SetTile(pos, mapSettingsData.GetTile(TileType.Floor));
            mapTilemaps[TileType.Wall].SetTile(pos + Vector3Int.down, mapSettingsData.GetTile(TileType.Wall));
            mapTilemaps[TileType.Wall].SetTile(pos + Vector3Int.up, mapSettingsData.GetTile(TileType.Wall));
        }
        
        private void PlaceVerticalBlock(Vector3Int pos, MapTilemaps mapTilemaps)
        {
            mapTilemaps[TileType.Floor].SetTile(pos, mapSettingsData.GetTile(TileType.Floor));
            mapTilemaps[TileType.Wall].SetTile(pos + Vector3Int.left, mapSettingsData.GetTile(TileType.Wall));
            mapTilemaps[TileType.Wall].SetTile(pos + Vector3Int.right, mapSettingsData.GetTile(TileType.Wall));
        }

        private bool DoCornersHasTile(Vector3Int pos, int halfWidth, int halfHeight, TileTypeTilemap map)
        {
            return map.HasAnyTileAt(pos + Vector3Int.right * halfWidth + Vector3Int.up * halfHeight) &&
                   map.HasAnyTileAt(pos + Vector3Int.left * halfWidth + Vector3Int.up * halfHeight) &&
                   map.HasAnyTileAt(pos + Vector3Int.right * halfWidth + Vector3Int.down * halfHeight) &&
                   map.HasAnyTileAt(pos + Vector3Int.left * halfWidth + Vector3Int.down * halfHeight);
        }

        private Vector3Int SelectDoor(DoorDir dir, RoomEntity roomEntity)
        {
            return roomEntity.DoorsDictionary.GetRandomDoor(dir);
        }
    }
}