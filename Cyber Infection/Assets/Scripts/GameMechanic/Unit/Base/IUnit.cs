using GameMechanic.Base;
using UnityEngine;

namespace GameMechanic.Unit.Base
{
	public interface IUnit : IAlive
	{
		Transform refTransform { get; }
	}
}