using CyberInfection.Data.Entities;
using CyberInfection.Extension.Pool;
using CyberInfection.GameMechanics.Unit.Player;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Projectile
{
	public class Bullet : PoolableMonoBehaviour
	{
		private SpriteRenderer m_SpriteRenderer;
		private Rigidbody2D m_Rigidbody;
		private Collider2D m_Collider;
		
		private float m_Speed;
		private float m_Damage;
		private float m_Lifetime = 5f;
		private Vector2 m_Direction;
		
		protected override void Awake()
		{
			base.Awake();
			m_SpriteRenderer = GetComponent<SpriteRenderer>();
			m_Rigidbody = GetComponent<Rigidbody2D>();
			m_Collider = GetComponent<Collider2D>();
		}

		public override void OnPop()
		{
			enabled = true;
			m_Collider.enabled = PhotonNetwork.IsMasterClient;
		}

		public override void OnPush()
		{
			enabled = false;
		}

		public virtual void InitializeParameters(BulletData parameters, Vector2 direction)
		{
			m_SpriteRenderer.sprite = parameters.sprite;
			m_Speed = parameters.speed;
			m_Damage = parameters.damage;
			m_Lifetime = 5f;
			m_Direction = direction;
			Debug.Log("<b>Bullet Parameters:</b> Speed = " + m_Speed + "; Damage = " + m_Damage + "; " +
			          "Direction = " + m_Direction.ToString("F3"));
		}

		private void Update()
		{
			if (m_Lifetime <= 0f)
			{
				Push();
			}

			m_Lifetime -= Time.deltaTime;
		}

		private void FixedUpdate()
		{
			m_Rigidbody.MovePosition(m_Rigidbody.position + m_Direction * Time.deltaTime * m_Speed);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			other.GetComponent<IAlive>()?.GetDamage(m_Damage);
			Push();
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			Push();
		}
	}
}
