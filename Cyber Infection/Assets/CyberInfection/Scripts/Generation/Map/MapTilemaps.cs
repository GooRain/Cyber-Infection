using System.Linq;
using CyberInfection.Generation.Tiles;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CyberInfection.Generation
{
    [System.Serializable]
    public class MapTilemaps
    {
        [SerializeField] private Tilemap floorTilemap;
        [SerializeField] private Tilemap wallTilemap;
        [SerializeField] private Tilemap shadowTilemap;
        [SerializeField] private GameObject doorPrefab;

        [SerializeField] private TileTypeTilemap tileTypeTilemap;

        public Tilemap ShadowTilemap => shadowTilemap;
        public Tilemap WallTilemap => wallTilemap;
        public Tilemap FloorTilemap => floorTilemap;

        public GameObject DoorPrefab => doorPrefab;

        public TileTypeTilemap TypeTilemap => tileTypeTilemap;

        public Tilemap this[TileType type] => tileTypeTilemap[type];

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
            shadowTilemap.ClearAllTiles();
        }

        public void RefreshAllTiles()
        {
            floorTilemap.RefreshAllTiles();
            wallTilemap.RefreshAllTiles();
            shadowTilemap.RefreshAllTiles();
        }
    }

    [System.Serializable]
    public class TileTypeTilemap : SerializableDictionaryBase<TileType, Tilemap>
    {
        public bool HasAnyTileAt(Vector3Int pos)
        {
            return Keys.Any(key => this[key].HasTile(pos));
        }
    }
}