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
		root = new Leaf((int)transform.position.x - MapSettings.Ins.mapSize.width / 2, (int)transform.position.y - MapSettings.Ins.mapSize.height / 2, MapSettings.Ins.mapSize.width, MapSettings.Ins.mapSize.height);
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

	private IEnumerator Split()
	{
		leafs.Add(root);

		bool did_split = true;
		while(did_split)
		{
			did_split = false;
			foreach(Leaf item in leafs)
			{
				if(item.leftChild == null && item.rightChild == null)
				{
					if(item.rect.width > MapSettings.Ins.MaxLeafSize || item.rect.height > MapSettings.Ins.MaxLeafSize || Random.Range(0f, 1f) > 0.25f)
					{
						if(item.Split())
						{
							leafs.Add(item.leftChild);
							leafs.Add(item.rightChild);
							did_split = true;
							break;
						}
					}
				}
			}
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
			Vector3 pos = new Vector3(item.Settings.Pos.X - item.Settings.Size.width / 2,
				item.Settings.Pos.X - item.Settings.Size.height / 2, 5f);
			Wall _wall = Instantiate(wallPrefab, pos, Quaternion.identity, map.transform);
			pos = new Vector3(item.Settings.Pos.X - item.Settings.Size.width / 2,
				item.Settings.Pos.X - item.Settings.Size.height / 2, 5f);
			_wall = Instantiate(wallPrefab, pos, Quaternion.identity, map.transform);
			pos = new Vector3(item.Settings.Pos.X - item.Settings.Size.width / 2,
				item.Settings.Pos.X - item.Settings.Size.height / 2, 5f);
			_wall = Instantiate(wallPrefab, pos, Quaternion.identity, map.transform);
			pos = new Vector3(item.Settings.Pos.X - item.Settings.Size.width / 2,
				item.Settings.Pos.X - item.Settings.Size.height / 2, 5f);
			_wall = Instantiate(wallPrefab, pos, Quaternion.identity, map.transform);
		}
		yield return true;
	}

	public void AddRoom(Leaf sender)
	{
		roomsSettings.Add(sender.room);
	}

}
