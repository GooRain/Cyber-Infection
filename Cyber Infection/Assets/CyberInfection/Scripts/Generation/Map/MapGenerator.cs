using System;
using System.Collections.Generic;
using CyberInfection.Constants;
using CyberInfection.Data.Settings.Generation;
using CyberInfection.Extension;
using CyberInfection.Generation.Room;
using CyberInfection.Generation.Tiles;
using Photon.Pun;
using UnityEngine;
using CyberInfection.UI.Radar;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Zenject;
using Random = UnityEngine.Random;

#if UNITY_EDITOR

#endif

namespace CyberInfection.Generation
{
    [RequireComponent(typeof(PhotonView))]
    public class MapGenerator : MonoBehaviour
    {
        public static MapGenerator instance;
        
        private PhotonView photonView;

        [SerializeField] private Transform _mapHolder;
        [SerializeField] private MapTilemaps tilemaps;
        [SerializeField] private ColorTileTypeData colorTileTypeData;
        [SerializeField] private RoomTypeTemplatesData roomTypeTemplatesData;

        /* 0000000
         * 0001100
         * 0002100
         * 0001000
         * 0031000
         */

        private MapSettingsData mapSettingsData;
        private MapController mapController;
        private int seed;
        private RoomTemplate[,] roomTemplates;
        private bool[,] generatedFlags;

        public Vector3 Offset { get; private set; }

        public Tilemap FloorTilemap => tilemaps.FloorTilemap;
        public Tilemap WallTilemap => tilemaps.WallTilemap;
        public Tilemap ShadowTilemap => tilemaps.ShadowTilemap;

        [Inject]
        private void Construct(MapSettingsData data)
        {
            mapSettingsData = data;
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            instance = this;
            photonView = GetComponent<PhotonView>();
            mapController = gameObject.AddComponent<MapController>();
            InitSeed();
        }

        private void Start()
        {
            if (PhotonNetwork.OfflineMode)
            {
                GenerateWithSeed(seed);
            }
            else if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("GenerateWithSeed", RpcTarget.AllBufferedViaServer, seed);
            }
        }

        [PunRPC]
        private void GenerateWithSeed(int seed)
        {
            Random.InitState(seed);
            if (TryToGenerate())
            {
                mapController.OnEndGenerate();
            }
            else
            {
                SceneManager.LoadScene(SceneName.Menu);
            }
        }

        private void InitSeed()
        {
            // Задаем семя для генерации ПСЧ
            var seedString = Guid.NewGuid();
            seed = seedString.GetHashCode();
            //_mapSettingsData.seed.GetHashCode();
            // SystemInfo.deviceModel + SystemInfo.deviceName;
            Random.InitState(seed);

            Debug.Log(seedString + " => " + seed);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Clear();
                TryToGenerate();
            }
        }

        private void Clear()
        {
            mapController.Clear();
            tilemaps.Clear();
        }

        private bool TryToGenerate()
        {
            Generate();
            return true;
        }

        private void Generate()
        {
            GenerateRoomTypes();
            GenerateRoomTemplates();
            PlaceRooms();

            tilemaps.RefreshAllTiles();
        }

        private void GenerateRoomTypes()
        {
            var maxRoomsAmount = (int) Random.Range(mapSettingsData.roomsRange.x, mapSettingsData.roomsRange.y);
            var roomTypes = Enum.GetValues(typeof(RoomType));

            var generatingEntitiesCount = Mathf.Sqrt(maxRoomsAmount);

            Offset = new Vector3(
                -mapSettingsData.mapSize.width * .5f * (mapSettingsData.roomSizeInfo.roomWidth - 1) - .5f,
                -mapSettingsData.mapSize.height * .5f * (mapSettingsData.roomSizeInfo.roomHeight - 1) - .5f);

            mapController.Initialize(mapSettingsData, Offset, _mapHolder);

            _mapHolder.position = Offset;

            var generatingEntities = new List<GeneratingEntity>();

            for (var i = 0; i < generatingEntitiesCount; i++)
            {
                generatingEntities.Add(new GeneratingEntity(ref mapController.map, new PointInt(
                    mapSettingsData.mapSize.width / 2,
                    mapSettingsData.mapSize.height / 2
                )));
            }
            
            Debug.Log("Entities count = " + generatingEntities.Count);

            var currentRoomsCount = 0;

            generatingEntities[0].PlaceRoom(RoomType.Start);

            while (currentRoomsCount < maxRoomsAmount && generatingEntities.Count > 0)
            {
                foreach (var generatingEntity in generatingEntities)
                {
                    generatingEntity.Move();

                    if (!generatingEntity.CanPlace()) continue;

                    var roomType = (RoomType) roomTypes.GetValue(Random.Range(2, roomTypes.Length));
                    if (mapController.map.HasEnd())
                    {
                        while (((roomType & ~RoomType.End & ~RoomType.Start) | RoomType.None) == 0)
                        {
                            roomType = (RoomType) roomTypes.GetValue(Random.Range(2, roomTypes.Length));
                        }

                        generatingEntity.PlaceRoom(roomType & ~RoomType.End);
                    }
                    else
                    {
                        generatingEntity.PlaceRoom(roomType);
                    }

                    currentRoomsCount++;
                }
            }
            
            if (RadarController.instance != null)
            {
                RadarController.instance.SetRoomsCount(currentRoomsCount);
            }
        }
        
        private void GenerateRoomTemplates()
        {
            var width = mapController.map.Width;
            var height = mapController.map.Height;
            roomTemplates = new RoomTemplate[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var roomType = mapController.map[x, y];
                    if (roomType != RoomType.None)
                    {
                        roomTemplates[x, y] = GetRoomTemplate(roomType);
                    }
                }
            }
        }

        private void PlaceRooms()
        {
            var corridorCreator = new CorridorCreator(mapSettingsData);
            var roomControllersHolder = new GameObject("RoomControllers Holder");
            roomControllersHolder.transform.SetParent(_mapHolder);
            Room.RoomEntity previousRoomEntity = null;
            var currentPosition = Vector3Int.zero;

            var map = mapController.map;
            var width = map.Width;
            var height = map.Height;

            generatedFlags = new bool[width, height];

            var center = new Vector2Int(width / 2, height / 2);
            var roomTemplate = roomTemplates[center.x, center.y];
            var newRoom = new RoomEntity(roomTemplate, map[center]);
            GenerateRoom(currentPosition, center);
            
//            for (var x = 0; x < width; x++)
//            {
//                for (var y = 0; y < height; y++)
//                {
//                    var roomType = mapController.map.roomMatrix[x, y];
//
//                    if ((roomType | RoomType.None) == 0) continue;
//
//                    var roomTemplate = roomTemplates[x, y];
//                    
//                    if (previousRoomEntity != null)
//                    {
//                        corridorCreator.PlaceCorridor(previousRoomEntity, newRoom, tilemaps, DoorDir.Top);
//                    }
//                    previousRoomEntity = newRoom;
//                }
//            }
        }

        private void GenerateRoom(Vector3Int pos, Vector2Int roomIndex)
        {
            var map = mapController.map;

            if (map[roomIndex + Vector2Int.right] != RoomType.None)
            {
                PlaceRoom(FindFreePosition(pos, Vector3Int.right), roomTemplates[roomIndex.x, roomIndex.y]);
            }
        }

        private Vector3Int FindFreePosition(Vector3Int fromPos, Vector3Int dir)
        {
            if(CorridorCreator
        }
        
        private void PlaceRoom(Vector3Int startPos, RoomTemplate template)
        {
            for (var x = 0; x < template.width; x++)
            {
                for (var y = 0; y < template.height; y++)
                {
                    var tile = template.tiles[x, y];
                    var tilePos = new Vector3Int(startPos.x + x, startPos.y + y, startPos.z);
                    tilemaps.TypeTilemap[tile].SetTile(tilePos, mapSettingsData.TileTypeTileDictionary[tile]);
//                    Debug.Log("X: " + x + " Y: " + y + " - " + tile);
                }
            }
        }
        
        private RoomTemplate GetRoomTemplate(RoomType roomType)
        {
            return new RoomTemplate(roomTypeTemplatesData.GetRandom(roomType), colorTileTypeData);
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}