using UnityEngine;

namespace CyberInfection.GameMechanics
{
	public interface IControllable
	{
		void Move(Vector2 direction);
		void Rotate(Vector2 direction);
	}
}