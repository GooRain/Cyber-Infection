using UnityEngine;

[System.Serializable]
public class Rectangle
{

	public int width;
	public int height;

	public Rectangle(int _width, int _height)
	{
		width = _width;
		height = _height;
	}

	public static Rectangle operator +(Rectangle r1, Rectangle r2)
	{
		return new Rectangle(r1.width + r2.width, r1.height + r2.height);
	}

	public Vector3 GetVector3(float _Z = 1f)
	{
		return new Vector3(width, _Z, height);
	}

}