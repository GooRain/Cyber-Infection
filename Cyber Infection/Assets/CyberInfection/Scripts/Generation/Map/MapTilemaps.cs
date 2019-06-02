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

        public Tilemap ShadowTilemap => shadowTilemap;
        public Tilemap WallTilemap => wallTilemap;
        public Tilemap FloorTilemap => floorTilemap;

		public GameObject DoorPrefab => doorPrefab;

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
}