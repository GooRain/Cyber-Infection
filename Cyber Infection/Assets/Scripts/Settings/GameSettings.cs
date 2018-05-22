using UnityEngine;

public class GameSettings : MonoBehaviour
{
	
	public static GameSettings Ins { get; private set; }
	private void Singleton()
	{
		if(Ins != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Ins = this;
			DontDestroyOnLoad(gameObject);
		}
	}


	private void Awake()
	{
		Singleton();

		DontDestroyOnLoad(gameObject);
	}

}
