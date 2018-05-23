using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

	public static MapGenerator Ins { get; private set; }
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

	[Header("References")]
	public Map map;
	[Header("Prefabs")]
	public Room roomPrefab;
	public Wall wallPrefab;

	private Leaf root;
	private List<Leaf> leafs;
	private List<RoomSettings> roomsSettings;
	private IEnumerator generationCoroutine;

	private void Awake()
	{
		Singleton();


	}

	private void Start()
	{
		generationCoroutine = Generate();
		StartCoroutine(generationCoroutine);
	}

	private IEnumerator Generate()
	{
		root = new Leaf((int)transform.position.x, (int)transform.position.y, MapSettings.Ins.mapSize.width, MapSettings.Ins.mapSize.height);
		leafs = new List<Leaf>();
		roomsSettings = new List<RoomSettings>();

		Debug.Log("Leafs initialization Is Done!");
		generationCoroutine = Split();
		yield return StartCoroutine(generationCoroutine);

		Debug.Log("Leafs splitting Is Done!");

		root.CreateRooms();

		Debug.Log("Rooms has been initialized!");

		generationCoroutine = PlaceRooms();
		yield return StartCoroutine(generationCoroutine);

		Debug.Log("Rooms placing Is Done!");

		generationCoroutine = PlaceWalls();
		yield return StartCoroutine(generationCoroutine);
		Debug.Log("Walls placing is Done!");
	}

	private bool CheckInput(KeyCode key)
	{
		return Input.GetKeyDown(key);
	}

	private IEnumerator Split()
	{
		leafs.Add(root);

		bool did_split = true;
		int index = 0;
		while(did_split)
		{
			yield return new WaitUntil(() => CheckInput(KeyCode.N));
			did_split = false;
			foreach(Leaf item in leafs)
			{
				if(item.leftChild == null && item.rightChild == null)
				{
					if(item.rect.width > MapSettings.Ins.MaxLeafSize || item.rect.height > MapSettings.Ins.MaxLeafSize || Random.Range(0f, 1f) > 0.25f)
					{
						if(item.Split())
						{
							index++;
							leafs.Add(item.leftChild);
							leafs.Add(item.rightChild);

							string left = "(" + item.leftChild.pos.X + ", " + item.leftChild.pos.Y + ") | (" + item.leftChild.rect.width + ", " + item.leftChild.rect.height + ")";
							string right = "(" + item.rightChild.pos.X + ", " + item.rightChild.pos.Y + ") | (" + item.rightChild.rect.width + ", " + item.rightChild.rect.height + ")";
							Debug.Log("Split #" + index + ": " + left + "   " + right);

							did_split = true;
							break;
						}
					}
				}
			}

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

	private void PlaceWall(Room _room, Vector3 _pos, Vector3 _scale)
	{
		Wall _wall = Instantiate(wallPrefab, _pos, Quaternion.identity, map.transform);
		_wall.transform.localScale = _scale;
		_room.Walls.Add(_wall);
		_wall.transform.SetParent(_room.transform);
	}

	public void AddRoom(Leaf sender)
	{
		roomsSettings.Add(sender.room);
	}

}
