using System;
using System.Collections.Generic;
using CyberInfection.Data.Settings.Generation;
using CyberInfection.Extension;
using CyberInfection.Generation.Room;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using CyberInfection.UI.Radar;
using UnityEngine.Tilemaps;
using Zenject;
using Random = UnityEngine.Random;

#if UNITY_EDITOR

#endif

namespace CyberInfection.Generation.Map
{
    [RequireComponent(typeof(PhotonView))]
    public class MapGenerator : MonoBehaviour
    {
        private PhotonView m_PhotonView;

        [SerializeField] private Transform _mapHolder;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Tilemap _collisionTileMap;

        /* 0000000
         * 0001100
         * 0002100
         * 0001000
         * 0031000
         */

        private MapSettingsData _mapSettingsData;
        private MapController _mapController;
        private int m_Seed;

        public Vector3 offset { get; private set; }

        [Inject]
        private void Construct(MapSettingsData mapSettingsData)
        {
            _mapSettingsData = mapSettingsData;
        }

        private void Awake()
        {
            m_PhotonView = GetComponent<PhotonView>();
            _mapController = gameObject.AddComponent<MapController>();

            if (PhotonNetwork.OfflineMode)
            {
                InitSeed();
                GenerateWithSeed(m_Seed);
            }
            else if (PhotonNetwork.IsMasterClient)
            {
                InitSeed();
                m_PhotonView.RPC("GenerateWithSeed", RpcTarget.AllBufferedViaServer, m_Seed);
            }
        }

        [PunRPC]
        private void GenerateWithSeed(int seed)
        {
            Random.InitState(seed);
            TryToGenerate();
        }

        private void InitSeed()
        {
            // Задаем семя для генерации ПСЧ
            var seedString = Guid.NewGuid();
            m_Seed = seedString.GetHashCode();
            //_mapSettingsData.seed.GetHashCode();
            // SystemInfo.deviceModel + SystemInfo.deviceName;
            Random.InitState(m_Seed);

            Debug.Log(seedString + " => " + m_Seed);
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
            _mapController.Clear();
            _tilemap.ClearAllTiles();
            _collisionTileMap.ClearAllTiles();
        }

        private bool TryToGenerate()
        {
            GenerateGraph();
            GenerateOld();
            return true;
        }

        private void GenerateGraph()
        {
            var maxRoomsAmount = (int) Random.Range(_mapSettingsData.roomsRange.x, _mapSettingsData.roomsRange.y);
            var mapGraph = new MapGraph(maxRoomsAmount);
        }

        [Obsolete]
        private void GenerateOld()
        {
//			Debug.Log("Generating...");

            var maxRoomsAmount = (int) Random.Range(_mapSettingsData.roomsRange.x, _mapSettingsData.roomsRange.y);
            var roomTypes = Enum.GetValues(typeof(RoomType));
//			for (var i = 0; i < roomTypes.Length; i++)
//			{
//				Debug.Log($"[{i}] = {roomTypes.GetValue(i)}");
//			}

            var generatingEntitiesCount = maxRoomsAmount / _mapSettingsData.roomsRange.x;

            offset = new Vector3(
                -_mapSettingsData.mapSize.width * .5f * (_mapSettingsData.roomSizeInfo.roomWidth - 1) - .5f,
                -_mapSettingsData.mapSize.height * .5f * (_mapSettingsData.roomSizeInfo.roomHeight - 1) - .5f);

            _mapController.Initialize(_mapSettingsData, offset, _mapHolder);

//			_tilemap.transform.position = offset;
//			_collisionTileMap.transform.position = offset;
            _mapHolder.position = offset;

            var generatingEntities = new List<GeneratingEntity>();

            for (var i = 0; i < generatingEntitiesCount; i++)
            {
                generatingEntities.Add(new GeneratingEntity(ref _mapController.map, new PointInt(
                    _mapSettingsData.mapSize.width / 2,
                    _mapSettingsData.mapSize.height / 2
                )));
            }

            var currentRoomsCount = 0;

            generatingEntities[0].PlaceRoom(RoomType.Start);

            while (currentRoomsCount < maxRoomsAmount && generatingEntities.Count > 0)
            {
                foreach (var generatingEntity in generatingEntities)
                {
                    generatingEntity.Move();

                    if (!generatingEntity.CanPlace()) continue;

                    var roomType = (RoomType) roomTypes.GetValue(Random.Range(2, roomTypes.Length));
                    if (_mapController.map.HasEnd())
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
            RadarController.instance.SetRoomsCount(currentRoomsCount);
            //if (_mapController.map.HasEnd())
            //{
            //    Debug.Log("Map has end!");

            //}

            _mapController.PlaceRooms(_tilemap, _collisionTileMap);

            _tilemap.RefreshAllTiles();
            _collisionTileMap.RefreshAllTiles();
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