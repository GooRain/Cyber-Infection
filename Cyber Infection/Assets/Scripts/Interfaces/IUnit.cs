using UnityEngine;

namespace Interfaces
{
	public interface IUnit : IAlive
	{
		Transform refTransform { get; }
	}
}