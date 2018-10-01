using Data;
using Data.Settings;
using Interfaces;
using UnityEngine;

namespace Controllers
{
	public class InputController
	{
		private InputSettingsData _settings;

		private IControllable _controllable;

		public void SetControllable(IControllable newControllable)
		{
			_controllable = newControllable;
		}

		private void Awake()
		{
			_settings = (InputSettingsData) InputSettingsData.instance;
		}

		private void Update()
		{
			HandleInput();
		}

		private void HandleInput()
		{
			if (Input.GetKeyDown(_settings.GetKeyCode("Left")))
			{
				Move(Vector3.left);
			}
		}

		private void Move(Vector3 motion)
		{
			_controllable.Move(motion);
		}
	}
}