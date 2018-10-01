﻿using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collider)
	{
		Player player = collider.GetComponent<Player>();
		if(!player)
			Destroy(gameObject);
	}
}
