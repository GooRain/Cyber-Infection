using UnityEngine;

namespace Entities
{
	public interface IControllable
	{
		void Move(Vector3 direction);
		void Rotate(Vector3 direction);
	}
}