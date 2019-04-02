using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.Components
{
    public class LocalShootComponent : IShootComponent
    {
        private WeaponController _weaponController;

        public LocalShootComponent(WeaponController weaponController)
        {
            _weaponController = weaponController;
        }
        
        public void Shoot(Vector2 direction)
        {
            _weaponController.RPCShoot(direction);
        }
    }
}