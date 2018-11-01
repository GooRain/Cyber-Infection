using Data.Generating;
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
		private GeneratingScenesData _data;

		private void Awake()
		{
			SceneManager.activeSceneChanged += OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene from, Scene to)
		{
			_data = GeneratingScenesData.instance;

			if (_data.DoGenerate(to))
			{
				Generate();
			}
		}

		private void Generate()
		{
			Debug.Log("Generating...");
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