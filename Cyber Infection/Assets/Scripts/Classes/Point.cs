using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Point
{

	public float x;
	public float y;

	public Point(float _X, float _Y)
	{
		x = _X;
		y = _Y;
	}

	public static Point operator +(Point p1, Point p2)
	{
		return new Point(p1.x + p2.x, p1.y + p2.y);
	}

	public Vector3 GetVector3(float z = 0f)
	{
		return new Vector3(x, y, z);
	}
}
