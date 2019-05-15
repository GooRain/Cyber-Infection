//#define SHOW_DEBUG

using CyberInfection.Data.Entities;
using CyberInfection.Extension.Pool;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Projectile
{
	public class Bullet : PoolableMonoBehaviour
	{
		private SpriteRenderer spriteRenderer;
		private Rigidbody2D myRigidbody;
		private Collider2D myCollider;
		
		private float speed;
		private int damage;
		private float lifetime = 5f;
		private Vector2 direction;
		
		protected override void Awake()
		{
			base.Awake();
			spriteRenderer = GetComponent<SpriteRenderer>();
			myRigidbody = GetComponent<Rigidbody2D>();
			myCollider = GetComponent<Collider2D>();
		}

		public override void OnPop()
		{
			enabled = true;
			myCollider.enabled = PhotonNetwork.IsMasterClient;
		}

		public override void OnPush()
		{
			enabled = false;
		}

		public virtual void InitializeParameters(BulletData parameters, Vector2 direction)
		{
			spriteRenderer.sprite = parameters.sprite;
			speed = parameters.speed;
			damage = parameters.damage;
			lifetime = 5f;
			this.direction = direction;
#if SHOW_DEBUG
			Debug.Log("<b>Bullet Parameters:</b> Speed = " + m_Speed + "; Damage = " + m_Damage + "; " +
			          "Direction = " + m_Direction.ToString("F3"));
#endif
		}

		private void Update()
		{
			if (lifetime <= 0f)
			{
				Push();
			}

			lifetime -= Time.deltaTime;
		}

		private void FixedUpdate()
		{
			myRigidbody.MovePosition(myRigidbody.position + Time.deltaTime * speed * direction);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			other.GetComponent<IAlive>()?.GetDamage(damage);
			Push();
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			other.gameObject.GetComponent<IAlive>()?.GetDamage(damage);
			Push();
		}
	}
}
