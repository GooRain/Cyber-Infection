using UnityEngine;

[System.Serializable]
public class Point
{

	public float X;
	public float Y;

	public Point(float _X, float _Y)
	{
		X = _X;
		Y = _Y;
	}

	public static Point operator +(Point p1, Point p2)
	{
		return new Point(p1.X + p2.X, p1.Y + p2.Y);
	}

	public Vector3 GetVector3(float _Z = 0f)
	{
		return new Vector3(X, _Z, Y);
	}
}
