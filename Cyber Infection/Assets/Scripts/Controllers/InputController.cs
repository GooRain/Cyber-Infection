using Data;
using Interfaces;
using UnityEngine;

namespace Controllers
{
	public class InputController
	{
		private InputSettings _settings;
		
		private IControllable _controllable;

		public void SetControllable(IControllable newControllable)
		{
			_controllable = newControllable;
		}

		private void Update()
		{
			HandleInput();
		}

		public void HandleInput()
		{
			if (Input.GetKey(_settings["Move"]))
			{
				_controllable.Move();	
			}
			
			if (Input.GetKey(_settings["Rotate"]))
			{
				_controllable.Rotate();	
			}
		}
	}
}