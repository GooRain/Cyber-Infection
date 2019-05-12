using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Entity
{
	public interface IUnit : IAlive
	{
		Transform cachedTransform { get; }
		PhotonView cachedPhotonView { get; }
	}
}