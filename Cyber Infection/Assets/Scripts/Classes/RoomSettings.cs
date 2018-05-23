using UnityEngine;

[System.Serializable]
public class RoomSettings
{

	public Point Pos { get; private set; }
	public Rectangle Size { get; private set; }
	public float Z { get; private set; }

	public RoomSettings(Point _pos, Rectangle _size, float _z = 0f)
	{
		Pos = _pos;
		Size = _size;
		Z = _z;
	}
}
