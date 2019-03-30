using CyberInfection.GameMechanics.Projectile;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Unit
{
	public class Unit : MonoBehaviourPun, IUnit
	{
		public class CachedRPC
		{
			public const string ReceiveDamage = "ReceiveDamage";
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
			var damageData = new DamageData
			{
				damage = damageAmount
			};
			
			photonView.RPC(CachedRPC.ReceiveDamage, RpcTarget.All, JsonUtility.ToJson(damageData));
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
	}
}