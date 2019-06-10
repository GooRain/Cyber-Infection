using System;
using System.Collections.Generic;
using System.Linq;
using CyberInfection.Constants;
using CyberInfection.Data.Settings.Generation;
using CyberInfection.Extension;
using CyberInfection.GameMechanics;
using CyberInfection.Generation.Room;
using CyberInfection.Generation.Tiles;
using Photon.Pun;
using UnityEngine;
using CyberInfection.UI.Radar;
using RotaryHeart.Lib.SerializableDictionary;
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
            
            //if (_mapController.map.HasEnd())
            //{
            //    Debug.Log("Map has end!");

            //}

            //mapController.PlaceRooms(tilemaps);
            PlaceRooms();

            tilemaps.RefreshAllTiles();
        }

        public void PlaceRooms()
        {
            var roomIndex = 0;
            var roomControllersHolder = new GameObject("RoomControllers Holder");
            roomControllersHolder.transform.SetParent(_mapHolder);

            for (var x = 0; x < mapController.map.width; x++)
            {
                for (var y = 0; y < mapController.map.height; y++)
                {
                    var currentPosition = new Vector3Int(
                        x * (mapSettingsData.roomSizeInfo.roomWidth - 1),
                        y * (mapSettingsData.roomSizeInfo.roomHeight - 1),
                        0
                    );

                    var roomType = mapController.map.roomMatrix[x, y];

                    if ((roomType | RoomType.None) == 0) continue;

                    var roomTemplate = GetRoomTemplate(roomType);
                    PlaceRoom(currentPosition + new Vector3Int(roomTemplate.width, roomTemplate.height, 0), roomTemplate);
                }
            }
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

        #region DEPRECATED

        //		// Статическая ссылка на этот объект
//		public static MapGenerator Ins { get; private set; }
//
//		// Метод паттерна SingletonMonobehaviour
//		private void SingletonMonobehaviour()
//		{
//			if (Ins != null)
//			{
//				Destroy(gameObject);
//			}
//			else
//			{
//				Ins = this;
//			}
//		}
//
//		// Название области переменных в редакторе
//		[Header("References")]
//		// Ссылка на компонент Map
//		public Map map;
//
//		[Header("Prefabs")]
//		// Ссылка на префаб комнаты
//		public RoomController roomPrefab;
//
//		// Ссылка на префаб стены
//		public WallTile wallPrefab;
//
//		// Начальная область
//		private Area _root;
//
//		// Список областей
//		private List<Area> _areas;
//
//		// Список настроек каждой комнаты
//		private List<RoomSettings> _roomsSettings;
//
//		// Ссылка на IEnumerator генерации
//		private IEnumerator _generationCoroutine;
//
//		// Метод Awake вызывается при загрузке сцены
//		private void Awake()
//		{
//			SingletonMonobehaviour();
//		}
//
//		// Метод Start вызывается в начале после метода Awake, до метода Update
//		private void Start()
//		{
//			_generationCoroutine = Generate();
//			StartCoroutine(_generationCoroutine);
//		}
//
//		// Генерация карты
//		private IEnumerator Generate()
//		{
//			// Инициализируем начальную область
//			_root = new Area((int) transform.position.x, (int) transform.position.y, MapSettings.instance.mapSize.width,
//				MapSettings.instance.mapSize.height);
//			// Инициализируем список областей
//			_areas = new List<Area>();
//			// Инициализируем список настроек комнат
//			_roomsSettings = new List<RoomSettings>();
//
//			Debug.Log("Areas initialization Is Done!");
//			// Вызываем короутин разделения областей
//			_generationCoroutine = Split();
//			yield return StartCoroutine(_generationCoroutine);
//
//			Debug.Log("Areas splitting Is Done!");
//			// Создаем настройки комнат
//			_root.CreateRooms();
//
//			Debug.Log("Rooms has been initialized!");
//			// Расстановка комнат
//			_generationCoroutine = PlaceRooms();
//			yield return StartCoroutine(_generationCoroutine);
//
//			Debug.Log("Rooms placing Is Done!");
//			// Расстановка стен
//			_generationCoroutine = PlaceWalls();
//			yield return StartCoroutine(_generationCoroutine);
//			Debug.Log("Walls placing is Done!");
//		}
//
//		// Проверка нажатия клавиши
//		private bool CheckInput(KeyCode key)
//		{
//			return Input.GetKeyDown(key);
//		}
//
//		// Метод деления
//		private IEnumerator Split()
//		{
//			// Добавляем начальную область
//			_areas.Add(_root);
//
//			// Булеан проверки было ли произведено деление
//			var didSplit = true;
//			// Индекс для счета кол-ва делений
//			var index = 0;
//			// Цикл деления
//			while (didSplit)
//			{
//				//yield return new WaitUntil(() => CheckInput(KeyCode.N));
//				didSplit = false;
//				foreach (var item in _areas)
//				{
//					if (item.leftChild != null || item.rightChild != null) continue;
//					if (item.rect.width <= MapSettings.instance.MaxAreaSize &&
//					    item.rect.height <= MapSettings.instance.MaxAreaSize &&
//					    !(Random.Range(0f, 1f) > 0.25f)) continue;
//					// Если область была поделена
//					if (!item.Split()) continue;
//					index++;
//					// Добавляем в список левого область
//					_areas.Add(item.leftChild);
//					// Добавляем в список правую область
//					_areas.Add(item.rightChild);
//
//					var left = "(" + item.leftChild.pos.X + ", " + item.leftChild.pos.Y + ") | (" +
//					           item.leftChild.rect.width + ", " + item.leftChild.rect.height + ")";
//					var right = "(" + item.rightChild.pos.X + ", " + item.rightChild.pos.Y + ") | (" +
//					            item.rightChild.rect.width + ", " + item.rightChild.rect.height + ")";
//					Debug.Log("Split #" + index + ": " + left + "   " + right);
//
//					didSplit = true;
//					break;
//				}
//
//				// Чистим прошлые области и добавляем новые
//				foreach (var item in map.rooms)
//				{
//					Destroy(item.gameObject);
//				}
//
//				map.rooms.Clear();
//				_roomsSettings.Clear();
//				_root.CreateRooms();
//				yield return StartCoroutine(PlaceRooms());
//				yield return new WaitForEndOfFrame();
//			}
//
//			yield return true;
//		}
//
//		// Расстановка комнат
//		private IEnumerator PlaceRooms()
//		{
//			foreach (var item in _roomsSettings)
//			{
//				var newRoom = Instantiate(roomPrefab, item.Pos.GetVector3(item.Z), Quaternion.identity, map.transform);
//				newRoom.Settings = item;
//				newRoom.transform.localScale = item.Size.GetVector3();
//				newRoom.SetRandomColor();
//				map.rooms.Add(newRoom);
//			}
//
//			yield return true;
//		}
//
//		// Расстановка стен
//		private IEnumerator PlaceWalls()
//		{
//			foreach (var item in map.rooms)
//			{
//				var z = 3f;
//				var pos = new Vector3(item.Settings.Pos.X - item.Settings.Size.width / 2f,
//					z, item.Settings.Pos.Y);
//				var scale = new Vector3(1, 1, item.Settings.Size.height);
//				PlaceWall(item, pos, scale);
//
//				pos = new Vector3(item.Settings.Pos.X + item.Settings.Size.width / 2f,
//					z, item.Settings.Pos.Y);
//				scale = new Vector3(1, 1, item.Settings.Size.height);
//				PlaceWall(item, pos, scale);
//
//				pos = new Vector3(item.Settings.Pos.X,
//					z, item.Settings.Pos.Y - item.Settings.Size.height / 2f);
//				scale = new Vector3(item.Settings.Size.width, 1, 1);
//				PlaceWall(item, pos, scale);
//
//				pos = new Vector3(item.Settings.Pos.X,
//					z, item.Settings.Pos.Y + item.Settings.Size.height / 2f);
//				scale = new Vector3(item.Settings.Size.width, 1, 1);
//				PlaceWall(item, pos, scale);
//			}
//
//			yield return true;
//		}
//
//		// Метод ставит стену
//		private void PlaceWall(RoomController room, Vector3 pos, Vector3 scale)
//		{
//			var newWall = Instantiate(wallPrefab, pos, Quaternion.identity, map.transform);
//			newWall.transform.LookAt(new Vector3(room.transform.position.x, newWall.transform.position.y,
//				room.transform.position.z));
//			room.Walls.Add(newWall);
//			newWall.transform.SetParent(room.transform);
//			newWall.transform.localScale = new Vector3(1f, newWall.transform.localScale.y, newWall.transform.localScale.z);
//		}
//
//		// Добавить комнату области
//		public void AddRoom(Area sender)
//		{
//			_roomsSettings.Add(sender.room);
//		}

        #endregion
    }
}