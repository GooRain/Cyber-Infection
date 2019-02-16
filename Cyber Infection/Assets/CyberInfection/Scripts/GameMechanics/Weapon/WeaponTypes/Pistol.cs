using CyberInfection.Data.Object;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
    [System.Serializable]
    public class Pistol : WeaponBase
    {
        public AudioSource audioSource;
        
        public Pistol(WeaponData data, Transform muzzle) : base(data, muzzle)
        {
            
        }

        protected override void Shoot()
        {
            if (CanShoot())
            {
                return;
            }

            _lastShootTime = Time.timeSinceLevelLoad;
            shootBehaviour.Shoot();
            //audioSource.Play();
            ammoContainer.Dec();
        }

        private bool CanShoot()
        {
            return _lastShootTime + weaponData.shootRate > Time.timeSinceLevelLoad && shootBehaviour.CanShoot() &&
                   ammoContainer.HasAny();
        }

        protected override void Reload()
        {
            if (ammoContainer.CanRestore())
            {
                ammoContainer.Restore();
            }
        }
    }
}