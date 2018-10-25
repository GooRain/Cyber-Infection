using Interfaces;
using UnityEngine;

namespace PlayerScripts.Controllers
{
	[RequireComponent(typeof(IUnit))]
	public class UnitController : MonoBehaviour, IControllable
	{
		private IUnit _unit;

		private void Awake()
		{
			_unit = GetComponent<IUnit>();
		}
		
		public void Move(Vector3 direction)
		{
			_unit.refTransform.Translate(direction * Time.deltaTime);
		}

		public void Rotate(Vector3 direction)
		{
			_unit.refTransform.rotation.SetLookRotation(direction);
		}
	}
}