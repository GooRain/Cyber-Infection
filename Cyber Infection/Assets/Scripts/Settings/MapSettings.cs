using UnityEngine;

public class MapSettings : MonoBehaviour
{

	public static MapSettings Ins { get; private set; }
	private void Singleton()
	{
		if(Ins != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Ins = this;
		}
	}

	private void Awake()
	{
		Singleton();
	}

	public float MinLeafSize = 6f;
	public float MaxLeafSize = 20f;
	public Rectangle mapSize;

	
}
