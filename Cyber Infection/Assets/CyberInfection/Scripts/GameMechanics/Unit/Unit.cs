using CyberInfection.GameMechanics.Projectile;
using CyberInfection.GameMechanics.Improvements.HealthPotion;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Unit
{
	public class Unit : MonoBehaviourPun, IUnit
	{
		public class CachedRPC
		{
			public const string ReceiveDamage = "ReceiveDamage";
            public const string ReceiveHealth = "ReceiveHealth";
        }

		public PhotonView cachedPhotonView => photonView;

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
			if (PhotonNetwork.OfflineMode)
			{
				health -= damageAmount;
				return;
			}
			
			var damageData = new DamageData
			{
				damage = damageAmount
			};
			
			photonView.RPC(CachedRPC.ReceiveDamage, RpcTarget.All, JsonUtility.ToJson(damageData));
		}

        public void RestoreHealth(float healthAmount)
        {
            if (PhotonNetwork.OfflineMode)
            {
                health += healthAmount;
                return;
            }

            var healthAmountData = new HealthAmountData
            {
                _healthAmount = healthAmount
            };

            photonView.RPC(CachedRPC.ReceiveHealth, RpcTarget.All, JsonUtility.ToJson(healthAmount));
        }

		public void Die()
		{
			PhotonNetwork.Destroy(gameObject);
		}

		[PunRPC]
		protected virtual void ReceiveDamage(string data)
		{
			var damageData = JsonUtility.FromJson<DamageData>(data);
			health -= damageData.damage;
		}

        [PunRPC]
        protected virtual void ReceiveHealth(string data)
        {
            var healthAmountData = JsonUtility.FromJson<HealthAmountData>(data);
            health += healthAmountData._healthAmount;
        }
    }
}