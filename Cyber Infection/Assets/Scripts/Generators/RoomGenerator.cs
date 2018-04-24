using UnityEngine;

[RequireComponent(typeof(Room))]
public class RoomGenerator : MonoBehaviour
{

	private Room room;

	private Vector2 center;
	private Vector2 leftDownCorner;

	private int width;
	private int height;

	private int doorsNumber;

	private int[] doorPlacement = { 0, 0, 0, 0 };
	private int myNeighbourIndex = -1;
	private Vector2[] offsets;

	public void AssignParameters()
	{
		room = GetComponent<Room>();
		height = MapSettings.ins.roomHeight;
		width = MapSettings.ins.roomWidth;
		center = transform.position;
		leftDownCorner = new Vector2(center.x - (width / 2), center.y - (height / 2));

		doorsNumber = Random.Range(1, 16);
		room.Doors = (Doors)doorsNumber;

		string binaryText = System.Convert.ToString(doorsNumber, 2).PadLeft(4, '0');
		Debug.Log("HEX DOOR: " + binaryText);
		for(int i = 0; i < 4; i++)
		{
			doorPlacement[i] = binaryText[i] - 48;
		}
		binaryText = doorPlacement[0].ToString() + doorPlacement[1] + doorPlacement[2] + doorPlacement[3];
	}

	public void GenerateBlocks()
	{
		AssignParameters();
		Debug.Log("Generating blocks for " + name);
		GenerateWalls();
		GenerateNeighbours();
	}

	public void GenerateBlocks(int neighbourIndex)
	{
		AssignParameters();
		if(neighbourIndex >= 2)
		{
			myNeighbourIndex = neighbourIndex - 2;
		}
		else
		if(neighbourIndex <= 1 && neighbourIndex >= 0)
		{
			myNeighbourIndex = neighbourIndex + 2;
		}
		Debug.Log("Generating blocks for " + name);
		GenerateWalls();
		GenerateNeighbours();
	}

	private void GenerateWalls()
	{
		Debug.Log(name + " { width: " + width + "; height:" + height + " }");
		for(int i = 0; i < 4; i++)
		{
			if(doorPlacement[i] == 1)
			{
				room.DoorIsFree[i] = true;
			}
		}
		if(myNeighbourIndex >= 0)
			room.DoorIsFree[myNeighbourIndex] = true;
		PlaceWall("Upper", new Vector2(0, height - MapSettings.ins.blockSize.y), new Vector2(MapSettings.ins.blockSize.x, 0), width, room.DoorIsFree[0]);
		PlaceWall("Left", new Vector2(0, 0), new Vector2(0, MapSettings.ins.blockSize.y), height, room.DoorIsFree[1]);
		PlaceWall("Down", new Vector2(0, 0), new Vector2(MapSettings.ins.blockSize.x, 0), width, room.DoorIsFree[2]);
		PlaceWall("Right", new Vector2(width - MapSettings.ins.blockSize.x, 0), new Vector2(0, MapSettings.ins.blockSize.y), height, room.DoorIsFree[3]);
	}

	private void PlaceWall(string name, Vector2 offset, Vector2 step, int size, bool withDoor)
	{
		Block wallPrefab = MapSettings.ins.FindBlocks(BlockType.Wall).RandomBlock();
		int from = size / 2, to = size / 2;
		if(withDoor)
		{
			from -= 1;
			to += 1;
		}
		for(int i = 0; i < from; i++)
		{
			Vector3 spawnPosition = new Vector2(leftDownCorner.x + step.x * i, leftDownCorner.y + step.y * i) + offset;
			room.Walls.Add(Instantiate(wallPrefab, spawnPosition, Quaternion.identity, transform));
			room.Walls[room.Walls.Count - 1].name = name + " Wall #" + (i + 1);
		}
		for(int i = to; i < size; i++)
		{
			Vector3 spawnPosition = new Vector2(leftDownCorner.x + step.x * i, leftDownCorner.y + step.y * i) + offset;
			room.Walls.Add(Instantiate(wallPrefab, spawnPosition, Quaternion.identity, transform));
			room.Walls[room.Walls.Count - 1].name = name + " Wall #" + (i + 1);
		}
	}

	private void GenerateNeighbours()
	{
		if(myNeighbourIndex >= 0)
			room.DoorIsFree[myNeighbourIndex] = false;
		System.Collections.Generic.List<Room> neighbours = new System.Collections.Generic.List<Room>();
		System.Collections.Generic.List<int> myIndex = new System.Collections.Generic.List<int>();
		for(int i = 0; i < 4; i++)
		{
			if(MapSettings.ins.CanSpawnRoom())
			{
				if(room.DoorIsFree[i])
				{
					Vector2 spawnPos = (Vector2)transform.position + MapGenerator.ins.Offsets[i];
					Room tempRoom = Instantiate(MapGenerator.ins.roomPrefab, spawnPos, Quaternion.identity, MapGenerator.ins.map.transform);
					tempRoom.name = "Room#" + (MapGenerator.ins.RoomIndex++) + "." + i;
					MapGenerator.ins.map.rooms.Add(tempRoom);

					neighbours.Add(tempRoom);
					myIndex.Add(i);
				}
			}
			else
			{
				return;
			}
		}
		for(int i = 0; i < neighbours.Count; i++)
		{
			neighbours[i].Generate(myIndex[i]);
		}
	}
}
