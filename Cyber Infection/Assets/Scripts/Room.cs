using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

	private RoomGenerator generator;
	private Doors doors = Doors.E;
	private bool[] doorIsFree = { false, false, false, false };

	//public void Generate()
	//{
	//	MapSettings.ins.RoomSpawned();
	//	generator = GetComponent<RoomGenerator>();
	//	generator.GenerateBlocks();
	//}

	public void Generate(int neighbourIndex = -1)
	{
		MapSettings.ins.RoomSpawned();
		generator = GetComponent<RoomGenerator>();
		generator.GenerateBlocks(neighbourIndex);
	}

	private List<Block> walls;

	public List<Block> Walls
	{
		get
		{
			if(walls == null)
				walls = new List<Block>();
			return walls;
		}
		protected set { walls = value; }
	}

	public Doors Doors
	{
		get
		{
			return doors;
		}

		set
		{
			doors = value;
			Debug.Log("Doors type: " + value);
		}
	}

	public bool[] DoorIsFree
	{
		get
		{
			return doorIsFree;
		}

		set
		{
			doorIsFree = value;
		}
	}
}
