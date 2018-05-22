using UnityEngine;

[System.Serializable]
public class Rectangle
{

	public float width;
	public float length;

	public Vector3 GetVector3(float _Z = 1f)
	{
		return new Vector3(width, _Z, length);
	}

}