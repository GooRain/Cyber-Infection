using CyberInfection.Data.Object;
using CyberInfection.GameMechanics.Container;
using CyberInfection.GameMechanics.Weapon.Behaviour;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
    [System.Serializable]
    public abstract class WeaponBase : IWeapon
    {
        public WeaponData weaponData;
        
        public AmmunitionContainer ammoContainer;
        public ShootBehaviour shootBehaviour;

        protected float _lastShootTime;
        
        public WeaponBase(WeaponData data, Transform muzzle)
        {
            weaponData = data;
            
            ammoContainer = new AmmunitionContainer(weaponData.maxAmmoInMagazine, weaponData.startAmmunitionSize);
            shootBehaviour = new ShootBehaviour(muzzle, weaponData.bulletPrefab);
        }
        
        public float recoil { get; set; }
        public void TryToShoot()
        {
            if (shootBehaviour.CanShoot())
            {
                Shoot();
            }
        }

        protected abstract void Shoot();
        
        public void TryToReload()
        {
            if (ammoContainer.CanRestore())
            {
                Reload();
            }
        }

        protected abstract void Reload();
    }
}