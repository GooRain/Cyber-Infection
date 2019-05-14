using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Entity
{
	[RequireComponent(typeof(IUnit))]
	public class UnitController : MonoBehaviour, IControllable, IPunObservable
	{
		private IUnit _unit;

		[SerializeField] private float m_WalkSpeed;

		[SerializeField] private float m_RunSpeed;

		public float walkSpeed => m_WalkSpeed;
		public float runSpeed => m_RunSpeed;

		private void Awake()
		{
			_unit = GetComponent<IUnit>();
		}
		
		public virtual void Move(Vector2 moveVector)
		{
			_unit.cachedTransform.Translate(moveVector * Time.deltaTime);
		}

		public virtual void Rotate(Vector2 direction)
		{
			_unit.cachedTransform.rotation.SetLookRotation(direction);
		}

		public virtual void Shoot()
		{
			
		}

		public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			
		}
	}
}