using CyberInfection.GameMechanics.Projectile;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Entity
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
			UnitsManager.instance.OnUnitSpawn(this);
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

		public void Die()
		{
			if (cachedPhotonView.IsMine)
			{
				PhotonNetwork.Destroy(gameObject);
			}
		}

		[PunRPC]
		protected virtual void ReceiveDamage(string data)
		{
			var damageData = JsonUtility.FromJson<DamageData>(data);
			health -= damageData.damage;
		}
	}
}