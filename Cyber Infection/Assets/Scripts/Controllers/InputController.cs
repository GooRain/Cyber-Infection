using Data;
using Data.Settings;
using Interfaces;
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

		private void GetMotion(string value)
		{
			switch (value)
			{
				case "Left":
					_moveDirection.x = -1;
					break;
				case "Right":
					_moveDirection.x = 1;
					break;
				case "Up":
					_moveDirection.y = 1;
					break;
				case "Down":
					_moveDirection.y = -1;
					break;
				default:
					_moveDirection = Vector3.zero;
					break;
			}
		}

		private void GetDirection(string value)
		{
			switch (value)
			{
				case "Left":
					_lookDirection.y = 180;
					break;
				case "Right":
					_lookDirection.y = 0;
					break;
				case "Up":
					_lookDirection.y = 270;
					break;
				case "Down":
					_lookDirection.y = 90;
					break;
				default:
					_lookDirection.y = 0;
					break;
			}
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