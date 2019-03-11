using UnityEngine;

namespace CyberInfection.GameMechanics.Unit
{
	[RequireComponent(typeof(IUnit))]
	public class UnitController : MonoBehaviour, IControllable
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
		
		public virtual void Move(Vector2 direction)
		{
			_unit.cachedTransform.Translate(direction * Time.deltaTime);
		}

		public virtual void Rotate(Vector2 direction)
		{
			_unit.cachedTransform.rotation.SetLookRotation(direction);
		}

		public virtual void Shoot()
		{
			
		}
	}
}