using Interfaces;
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
				if(_health <= 0)
					Die();
			}
		}

		public void GetDamage(int damageAmount)
		{
			health -= damageAmount;
		}

		public void Die()
		{
			Destroy(gameObject);
		}

		public void Move()
		{
			
		}

		public void Rotate()
		{
			
		}

	}
}