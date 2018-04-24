using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public static MapGenerator ins;

	private void Singleton()
	{
		if(ins != null)
			Destroy(this);
		else
			ins = this;
	}

	public Map map;
	public Room roomPrefab;

	private Vector2 startPosition;
	private int roomIndex = 0;

	public Vector2[] Offsets { get; private set; }

	public int RoomIndex
	{
		get
		{
			return roomIndex;
		}

		set
		{
			roomIndex = value;
		}
	}

	private void Awake()
	{
		Singleton();
	}

	private void Start()
	{
		InitParameters();
		Generate();
	}

	private void InitParameters()
	{
		startPosition = GameSettings.ins.startPosition;
		map.transform.position = startPosition;
		Offsets = new Vector2[4];
		Offsets[0] = new Vector2(0, MapSettings.ins.blockSize.y * (MapSettings.ins.roomHeight));
		Offsets[1] = new Vector2(-MapSettings.ins.blockSize.x * (MapSettings.ins.roomWidth), 0);
		Offsets[2] = new Vector2(0, -MapSettings.ins.blockSize.y * (MapSettings.ins.roomHeight));
		Offsets[3] = new Vector2(MapSettings.ins.blockSize.x * (MapSettings.ins.roomWidth), 0);
	}

	private void Generate()
	{
		Vector3 spawnPos = map.transform.position;
		Room newRoom = Instantiate(roomPrefab, spawnPos, Quaternion.identity, map.transform);
		newRoom.name = "Room#" + roomIndex++;
		newRoom.Generate();
		map.rooms.Add(newRoom);
		MapSettings.ins.RoomSpawned();

		//while(newRoom.HaveFreeDoors)
		//{
		//	for(int i = 0; i < 4; i++)
		//	{
		//		if(newRoom.DoorIsFree[i])
		//		{
		//			spawnPos = (Vector2)newRoom.transform.position + offsets[i];
		//			Room tempRoom = Instantiate(roomPrefab, spawnPos, Quaternion.identity, map.transform);
		//			tempRoom.name = "Room#" + roomIndex++;
		//			tempRoom.Generate();
		//			map.rooms.Add(tempRoom);
		//		}
		//	}
		//	newRoom.HaveFreeDoors = false;
		//}
	}

}
