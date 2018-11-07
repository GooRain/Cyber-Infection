using System.Collections.Generic;

namespace Generation.Map
{
	public class Map
	{

		private List<Room.Room> _roomsList;

		public List<Room.Room> roomsList => _roomsList;

		public Map()
		{
			_roomsList = new List<Room.Room>();
		}

		public void Add(Room.Room newRoom)
		{
			var newRoomTransform = newRoom.transform;
			newRoomTransform.position = newRoom.Settings.Pos.GetVector3();
			_roomsList.Add(newRoom);
		}

		public Room.Room Get(int index)
		{
			return _roomsList[index];
		}

		public bool Remove(Room.Room room)
		{
			return _roomsList.Remove(room);
		}

		public void Clear()
		{
			_roomsList.RemoveRange(0, _roomsList.Count);
		}

	}
}
