using UnityEngine;

namespace CyberInfection.GameMechanics
{
	public interface IControllable
	{
		void Move(Vector2 moveVector);
		void Rotate(Vector2 direction);
	}
}