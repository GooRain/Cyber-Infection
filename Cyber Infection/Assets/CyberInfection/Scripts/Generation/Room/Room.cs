using CyberInfection.Extension;

namespace CyberInfection.Generation.Room
{
	public class Room
	{
		public RoomType type { get; set; }
		
		public Room(RoomType type)
		{
			this.type = type;
		}
	}
}