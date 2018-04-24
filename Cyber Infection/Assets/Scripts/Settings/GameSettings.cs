using UnityEngine;

public class GameSettings : MonoBehaviour
{

	//_______________SINGLETON________________
	public static GameSettings ins;

	private void Awake()
	{
		if(ins != null)
			Destroy(gameObject);
		else
		{
			ins = this;
		}
	}
	//________________________________________

	public Vector2 startPosition;

}
