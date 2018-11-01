using System.Collections.Generic;
using System.Linq;
using Data.Generating;
using Data.Settings;
using Persistent;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Generators
{
	public class MapGenerator : SingletonMonobehaviour<MapGenerator>
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeBeforeLoad()
		{
			InstantiateSingleton();
		}

		private MapSettingsData _mapSettingsData;
		
		private GeneratingScenesData _generatingScenesData;

		private Map _map;
		
		private void Awake()
		{
			SceneManager.activeSceneChanged += OnSceneLoaded;
			_mapSettingsData = MapSettingsData.instance;
		}

		private void Update()
		{
			if (Input.GetKey(KeyCode.G))
			{
				Clear();
				Generate();
			}
		}

		private void OnSceneLoaded(Scene from, Scene to)
		{
			_generatingScenesData = GeneratingScenesData.instance;

			if (_generatingScenesData.DoGenerate(to.name))
			{
				Generate();
			}
		}

		private void Clear()
		{
			foreach (var room in _map.roomsList)
			{
				Destroy(room.gameObject);
			}
			_map.Clear();
		}
		
		private void Generate()
		{
			Debug.Log("Generating...");
			_map = new Map();
			var mapSize = _mapSettingsData.mapSize;
			var roomsAmount = Random.Range(_mapSettingsData.roomsRange.x, _mapSettingsData.roomsRange.y);
			var previousRoomSettings = new RoomSettings(new Point(0, 0), new Rectangle(0, 0), 0);
			var previousOffset = new Point(Random.Range(0, 1) > 0 ? 1 : -1, Random.Range(0, 1) > 0 ? 1 : -1);
			for (var i = 0; i < roomsAmount; i++)
			{
				var roomSize = new Rectangle
				(
					Random.Range(_mapSettingsData.roomSizeInfo.minRoomWidth,
						_mapSettingsData.roomSizeInfo.maxRoomWidth),
					Random.Range(_mapSettingsData.roomSizeInfo.minRoomHeight,
						_mapSettingsData.roomSizeInfo.maxRoomHeight)
				);
				
				var newRoom = new GameObject("Room#" + i).AddComponent<Room>();
				var currentOffset = new Point(previousOffset.x > 0 ? 1 : -1, previousOffset.y > 0 ? 1 : -1);
				newRoom.Settings =
					new RoomSettings(
						new Point(
							previousRoomSettings.Pos.x +
							currentOffset.x * (previousRoomSettings.Size.width + roomSize.width) / 2f,
							previousRoomSettings.Pos.y +
							currentOffset.y * (previousRoomSettings.Size.height + roomSize.height) / 2f),
						new Rectangle(roomSize.width, roomSize.height));
				newRoom.meshRenderer = newRoom.gameObject.AddComponent<MeshRenderer>();
				var roomMesh = newRoom.gameObject.AddComponent<MeshFilter>().mesh = new Mesh();

				previousOffset = currentOffset;
				
				var newVertices = new List<Vector3>();
				var newUVs = new List<Vector2>();
				var newTriangles = new List<int>();
				
				for (var j = -1; j <= 1; j +=2)
				{
					newVertices.Add(new Vector3( roomSize.width * .5f, j * roomSize.height * .5f, 0f));
					newVertices.Add(new Vector3(-roomSize.width * .5f, j * roomSize.height * .5f, 0f));
				}

				for (var j = 0; j < newVertices.Count; j++)
				{
					newUVs.Add(new Vector2(j, j + 1));
				}
				
				for (var j = 0; j < newVertices.Count / 4; j++)
				{
					for (var t = j; t < 3; t++)
					{
						newTriangles.Add(t);
					}

					for (var t = j + 3; t > j; t--)
					{
						newTriangles.Add(t);
					}
				}

				roomMesh.vertices = newVertices.ToArray();
				roomMesh.uv = newUVs.ToArray();
				roomMesh.triangles = newTriangles.ToArray();
				newRoom.meshRenderer.material = _mapSettingsData.GetFloorMaterial();
				newRoom.meshRenderer.material.color = _mapSettingsData.GetColor(Random.Range(0f, 1f));
				
				_map.Add(newRoom);
				previousRoomSettings = newRoom.Settings;
			}
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
//		public Room roomPrefab;
//
//		// Ссылка на префаб стены
//		public Wall wallPrefab;
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
//		private void PlaceWall(Room room, Vector3 pos, Vector3 scale)
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