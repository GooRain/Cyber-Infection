using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	// Статическая ссылка на этот объект
	public static MapGenerator Ins { get; private set; }
	// Метод паттерна Singleton
	private void Singleton()
	{
		if(Ins != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Ins = this;
		}
	}

	// Название области переменных в редакторе
	[Header("References")]
	// Ссылка на компонент Map
	public Map map;
	[Header("Prefabs")]
	// Ссылка на префаб комнаты
	public Room roomPrefab;
	// Ссылка на префаб стены
	public Wall wallPrefab;

	// Начальная область
	private Area root;
	// Список областей
	private List<Area> areas;
	// Список настроек каждой комнаты
	private List<RoomSettings> roomsSettings;
	// Ссылка на IEnumerator генерации
	private IEnumerator generationCoroutine;

	// Метод Awake вызывается при загрузке сцены
	private void Awake()
	{
		Singleton();
	}

	// Метод Start вызывается в начале после метода Awake, до метода Update
	private void Start()
	{
		generationCoroutine = Generate();
		StartCoroutine(generationCoroutine);
	}
	
	// Генерация карты
	private IEnumerator Generate()
	{
		// Инициализируем начальную область
		root = new Area((int)transform.position.x, (int)transform.position.y, MapSettings.Ins.mapSize.width, MapSettings.Ins.mapSize.height);
		// Инициализируем список областей
		areas = new List<Area>();
		// Инициализируем список настроек комнат
		roomsSettings = new List<RoomSettings>();

		Debug.Log("Areas initialization Is Done!");
		// Вызываем короутин разделения областей
		generationCoroutine = Split();
		yield return StartCoroutine(generationCoroutine);

		Debug.Log("Areas splitting Is Done!");
		// Создаем настройки комнат
		root.CreateRooms();

		Debug.Log("Rooms has been initialized!");
		// Расстановка комнат
		generationCoroutine = PlaceRooms();
		yield return StartCoroutine(generationCoroutine);

		Debug.Log("Rooms placing Is Done!");
		// Расстановка стен
		generationCoroutine = PlaceWalls();
		yield return StartCoroutine(generationCoroutine);
		Debug.Log("Walls placing is Done!");
	}

	// Проверка нажатия клавиши
	private bool CheckInput(KeyCode key)
	{
		return Input.GetKeyDown(key);
	}

	// Метод деления
	private IEnumerator Split()
	{
		// Добавляем начальную область
		areas.Add(root);

		// Булеан проверки было ли произведено деление
		bool did_split = true;
		// Индекс для счета кол-ва делений
		int index = 0;
		// Цикл деления
		while(did_split)
		{
			yield return new WaitUntil(() => CheckInput(KeyCode.N));
			did_split = false;
			foreach(Area item in areas)
			{
				if(item.leftChild == null && item.rightChild == null)
				{
					if(item.rect.width > MapSettings.Ins.MaxAreaSize || item.rect.height > MapSettings.Ins.MaxAreaSize || Random.Range(0f, 1f) > 0.25f)
					{
						// Если область была поделена
						if(item.Split())
						{
							index++;
							// Добавляем в список левого область
							areas.Add(item.leftChild);
							// Добавляем в список правую область
							areas.Add(item.rightChild);

							string left = "(" + item.leftChild.pos.X + ", " + item.leftChild.pos.Y + ") | (" + item.leftChild.rect.width + ", " + item.leftChild.rect.height + ")";
							string right = "(" + item.rightChild.pos.X + ", " + item.rightChild.pos.Y + ") | (" + item.rightChild.rect.width + ", " + item.rightChild.rect.height + ")";
							Debug.Log("Split #" + index + ": " + left + "   " + right);
							
							did_split = true;
							break;
						}
					}
				}
			}

			// Чистим прошлые области и добавляем новые
			foreach(var item in map.rooms)
			{
				Destroy(item.gameObject);
			}
			map.rooms.Clear();
			roomsSettings.Clear();
			root.CreateRooms();
			yield return StartCoroutine(PlaceRooms());
			yield return new WaitForEndOfFrame();
		}

		yield return true;
	}

	// Расстановка комнат
	private IEnumerator PlaceRooms()
	{
		foreach(var item in roomsSettings)
		{
			Room _room = Instantiate(roomPrefab, item.Pos.GetVector3(item.Z), Quaternion.identity, map.transform);
			_room.Settings = item;
			_room.transform.localScale = item.Size.GetVector3();
			_room.SetRandomColor();
			map.rooms.Add(_room);
		}
		yield return true;
	}

	// Расстановка стен
	private IEnumerator PlaceWalls()
	{
		foreach(var item in map.rooms)
		{
			float _Z = 3f;
			Vector3 pos = new Vector3(item.Settings.Pos.X - item.Settings.Size.width / 2,
				_Z, item.Settings.Pos.Y);
			Vector3 scale = new Vector3(1, 1, item.Settings.Size.height);
			PlaceWall(item, pos, scale);

			pos = new Vector3(item.Settings.Pos.X + item.Settings.Size.width / 2,
				_Z, item.Settings.Pos.Y);
			scale = new Vector3(1, 1, item.Settings.Size.height);
			PlaceWall(item, pos, scale);

			pos = new Vector3(item.Settings.Pos.X,
				_Z, item.Settings.Pos.Y - item.Settings.Size.height / 2);
			scale = new Vector3(item.Settings.Size.width, 1, 1);
			PlaceWall(item, pos, scale);

			pos = new Vector3(item.Settings.Pos.X,
				_Z, item.Settings.Pos.Y + item.Settings.Size.height / 2);
			scale = new Vector3(item.Settings.Size.width, 1, 1);
			PlaceWall(item, pos, scale);
		}
		yield return true;
	}

	// Метод ставит стену
	private void PlaceWall(Room _room, Vector3 _pos, Vector3 _scale)
	{
		Wall _wall = Instantiate(wallPrefab, _pos, Quaternion.identity, map.transform);
		_wall.transform.LookAt(new Vector3(_room.transform.position.x, _wall.transform.position.y, _room.transform.position.z));
		_room.Walls.Add(_wall);
		_wall.transform.SetParent(_room.transform);
		_wall.transform.localScale = new Vector3(1f, _wall.transform.localScale.y, _wall.transform.localScale.z);
	}

	// Добавить комнату области
	public void AddRoom(Area sender)
	{
		roomsSettings.Add(sender.room);
	}

}
