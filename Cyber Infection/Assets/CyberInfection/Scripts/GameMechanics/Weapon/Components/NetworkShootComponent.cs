using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.Components
{
    public class NetworkShootComponent : IShootComponent
    {
        private PhotonView _photonView;
        
        public NetworkShootComponent(PhotonView photonView)
        {
            _photonView = photonView;
        }
        
        public void Shoot(Vector2 direction)
        {
            _photonView.RPC(WeaponController.CachedRPC.Shoot, RpcTarget.All, direction);
        }
    }
}