using UnityEngine;

namespace Extension
{
	[System.Serializable]
	public class Rectangle
	{

		public float width;
		public float height;

		public Rectangle(float width, float height)
		{
			this.width = width;
			this.height = height;
		}

		public static Rectangle operator +(Rectangle r1, Rectangle r2)
		{
			return new Rectangle(r1.width + r2.width, r1.height + r2.height);
		}

		public Vector3 GetVector3(float z = 1f)
		{
			return new Vector3(width, z, height);
		}

	}
}