using Data;
using Data.Settings;
using Entities;
using PlayerScripts.Controllers;
using UnityEngine;

namespace Controllers
{
	public class InputController : MonoBehaviour
	{
		private InputSettingsData _settings;

		private IControllable _controllable;

		private Vector3 _moveDirection;
		private Vector3 _lookDirection;

		public void SetControllable(IControllable newControllable)
		{
			_controllable = newControllable;
		}

		private void Awake()
		{
			_settings = InputSettingsData.instance;
		}

		private void Start()
		{
			SetControllable(FindObjectOfType<UnitController>());
		}

		private void Update()
		{
			HandleInput();
		}

		private void HandleInput()
		{
			Move(_moveDirection);
			Rotate(_lookDirection);
		}

		private void GetMotion(string value) //	Какой-нибудь GetButtonDown или GetAxis
		{
			//	Задать направление движения
		}

		private void GetDirection(string value) //	Какой-нибудь GetButtonDown или GetAxis
		{
			//	Задать направление просмотра
		}

		private void Move(Vector3 motion)
		{
			if (_controllable != null)
				_controllable.Move(motion);
		}

		private void Rotate(Vector3 direction)
		{
			if (_controllable != null)
				_controllable.Rotate(direction);
		}
	}
}