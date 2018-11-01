﻿using Entities;
using UnityEngine;

namespace PlayerScripts
{
	public class Unit : MonoBehaviour, IUnit
	{
		private int _health;

		public int health
		{
			get { return _health; }
			set
			{
				_health = value;
				if (_health <= 0)
					Die();
			}
		}

		public Transform refTransform { get; private set; }

		private void Awake()
		{
			CacheReferences();
		}

		private void CacheReferences()
		{
			refTransform = transform;
		}

		public void GetDamage(int damageAmount)
		{
			health -= damageAmount;
		}

		public void Die()
		{
			Destroy(gameObject);
		}
	}
}