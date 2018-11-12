﻿using GameMechanic.Unit.Player;
using UnityEngine;

namespace GameMechanic.Bullet
{
	public class Bullet : MonoBehaviour
	{

		private void OnTriggerEnter2D(Collider2D collider)
		{
			Player player = collider.GetComponent<Player>();
			if(!player)
				Destroy(gameObject);
		}
	}
}