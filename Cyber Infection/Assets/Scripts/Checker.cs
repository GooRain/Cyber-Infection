using UnityEngine;

public class Checker : MonoBehaviour
{

	private void Start()
	{
		Destroy(gameObject, 5f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.GetComponent<Checker>())
			Destroy(gameObject.GetComponentInParent<GameObject>());
	}

}
