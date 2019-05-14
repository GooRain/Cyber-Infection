namespace CyberInfection.Extension
{
	[System.Serializable]
	public class RoomSettings
	{
		public Point Pos { get; private set; }
		public Rectangle Size { get; private set; }
		public float Z { get; private set; }

		public RoomSettings(Point pos, Rectangle size, float z = 0f)
		{
			Pos = pos;
			Size = size;
			Z = z;
		}
	}
}
