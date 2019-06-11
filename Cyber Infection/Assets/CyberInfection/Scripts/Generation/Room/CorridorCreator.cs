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

        public void PlaceCorridor(Room from, Room to, MapTilemaps mapTilemaps
        , DoorDir dir)
        {
            var fromDoor = SelectDoor(dir, from);
            var toDoor = SelectDoor(dir.Invert(), to);
            
            var moveDir = dir.GetMoveDir();
            var offset = moveDir * MIN_LENGTH;
            var length = MIN_LENGTH;
            var pos = fromDoor + offset;
            
            while (!IsCornersEmpty(pos, from.Template.width, from.Template.height, mapTilemaps.TypeTilemap))
            {
                pos += moveDir;
                length++;
            }

            for (var i = 0; i < length; i++)
            {
                PlaceBlock(fromDoor + moveDir * i, moveDir, mapTilemaps);
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

        private bool IsCornersEmpty(Vector3Int pos, int width, int height, TileTypeTilemap map)
        {
            return map.HasAnyTileAt(fromDoor + offset);
        }

        private Vector3Int SelectDoor(DoorDir dir, Room room)
        {
            return room.DoorsDictionary.GetRandomDoor(dir);
        }
    }
}