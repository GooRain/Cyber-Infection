using UnityEngine;

namespace CyberInfection.GameMechanics.Unit
{
	public class Unit : MonoBehaviour, IUnit
	{
		private float _health;

		public float health
		{
			get => _health;
			set
			{
				_health = value;
				if (_health <= 0f)
					Die();
			}
		}

		public Transform cachedTransform { get; private set; }

		protected virtual void Awake()
		{
			CacheReferences();
		}

		private void CacheReferences()
		{
			cachedTransform = transform;
		}

		public void GetDamage(float damageAmount)
		{
			health -= damageAmount;
		}

		public void Die()
		{
			Destroy(gameObject);
		}
	}
}