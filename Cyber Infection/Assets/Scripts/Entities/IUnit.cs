using UnityEngine;

namespace Entities
{
	public interface IUnit : IAlive
	{
		Transform refTransform { get; }
	}
}