using CyberInfection.Data.Entities;
using CyberInfection.GameMechanics.Container;
using CyberInfection.GameMechanics.Projectile;
using CyberInfection.GameMechanics.Weapon.Behaviour;
using UnityEngine;
using Zenject;

namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
    [System.Serializable]
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        public WeaponData weaponData;
        
        public AmmunitionContainer ammoContainer;
        public ShootBehaviour shootBehaviour;

        protected float _lastShootTime;
        
        public virtual void Initialize(WeaponData data, Transform muzzle)
        {
            weaponData = data;
            
            ammoContainer = new AmmunitionContainer(weaponData.maxAmmoInMagazine, weaponData.startAmmunitionSize);
            shootBehaviour = new ShootBehaviour(muzzle, data.bulletData);
        }
        
        public float recoil { get; set; }
        public void TryToShoot()
        {
            if (CanShoot())
            {
                Shoot();
            }
        }

        protected virtual bool CanShoot()
        {
            return shootBehaviour.CanShoot();
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