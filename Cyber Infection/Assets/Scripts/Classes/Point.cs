using UnityEngine;

public class Point {

	public float X;
	public float Y;

	public Vector3 GetVector3(float _Z = 0f)
	{
		return new Vector3(X, _Z, Y);
	}
}
