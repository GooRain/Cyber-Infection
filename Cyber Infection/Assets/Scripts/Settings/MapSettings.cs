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

	public int MinLeafSize = 6;
	public int MaxLeafSize = 20;
	public Rectangle mapSize;

	
}
