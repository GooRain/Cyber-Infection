using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Unit
{
	public interface IUnit : IAlive
	{
		Transform cachedTransform { get; }
		PhotonView cachedPhotonView { get; }
	}
}