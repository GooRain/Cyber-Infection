using CyberInfection.Extension;

namespace CyberInfection.Generation.Room
{
	public class Room
	{
		private RoomTemplate template;
		private DoorDirDoorsDictionary doorsDictionary;
		public RoomType type;

		public DoorDirDoorsDictionary DoorsDictionary => doorsDictionary;
		public RoomTemplate Template => template;
		
		public Room(RoomTemplate template, RoomType type)
		{
			this.type = type;
			doorsDictionary = new DoorDirDoorsDictionary();
		}
	}
}