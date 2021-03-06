using CyberInfection.Data.Entities;
using CyberInfection.GameMechanics.Container;
using CyberInfection.GameMechanics.Weapon.Behaviour;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
    [System.Serializable]
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        public WeaponData weaponData;
        public Transform muzzle;
        
        protected ShootBehaviour shootBehaviour;

        protected float lastShootTime;
        
        public AmmunitionContainer AmmoContainer { get; protected set; }
        
        public virtual void Initialize(WeaponData data)
        {
            weaponData = data;
            
            AmmoContainer = new AmmunitionContainer(weaponData.maxAmmoInMagazine, weaponData.startAmmunitionSize);
            shootBehaviour = new ShootBehaviour(muzzle, data.bulletData);
        }
        
        public float recoil { get; set; }

        public virtual bool CanShoot()
        {
            return shootBehaviour.CanShoot();
        }
        
        [PunRPC]
        public abstract void Shoot(Vector2 direction);
        
        public void TryToReload()
        {
            if (AmmoContainer.CanRestore())
            {
                Reload();
            }
        }

        public abstract void Reload();
    }
}