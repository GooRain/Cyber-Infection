using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[SerializeField]
	private float maxDistance;
	[SerializeField]
	private float bulletSpeed = 7;

	private Vector2 startPos;
	private float currentDistance = 0f;

	private void Start()
	{
		startPos = transform.position;
	}

	private void Update()
	{
		transform.Translate(Vector2.up * Time.deltaTime * bulletSpeed);          // ВПЕРЕД ЛЕТИТ И ТОЛЬКО ВПЕРЕД
		currentDistance = Vector2.Distance(transform.position, startPos);                                    // Дистанцию чекает

		if(currentDistance >= maxDistance)
			Destroy(gameObject);
	}
}
