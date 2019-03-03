using UnityEngine;

namespace CyberInfection.GameMechanics.Unit
{
	[RequireComponent(typeof(IUnit))]
	public class UnitController : MonoBehaviour, IControllable
	{
		private IUnit _unit;

		private void Awake()
		{
			_unit = GetComponent<IUnit>();
		}
		
		public virtual void Move(Vector2 direction)
		{
			_unit.refTransform.Translate(direction * Time.deltaTime);
		}

		public virtual void Rotate(Vector2 direction)
		{
			_unit.refTransform.rotation.SetLookRotation(direction);
		}
	}
}