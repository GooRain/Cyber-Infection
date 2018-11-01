using System.Collections.Generic;
using UnityEngine;

public class Map
{

	private List<Room> _roomsList;

	public List<Room> roomsList => _roomsList;

	public Map()
	{
		_roomsList = new List<Room>();
	}

	public void Add(Room newRoom)
	{
		var newRoomTransform = newRoom.transform;
		newRoomTransform.position = newRoom.Settings.Pos.GetVector3();
		_roomsList.Add(newRoom);
	}

	public Room Get(int index)
	{
		return _roomsList[index];
	}

	public bool Remove(Room room)
	{
		return _roomsList.Remove(room);
	}

	public void Clear()
	{
		_roomsList.RemoveRange(0, _roomsList.Count);
	}

}
