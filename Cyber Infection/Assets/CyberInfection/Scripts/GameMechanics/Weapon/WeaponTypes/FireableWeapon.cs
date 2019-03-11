using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
    [System.Serializable]
    public class FireableWeapon : WeaponBase
    {
        public AudioSource audioSource;

        protected override void Shoot()
        {
            _lastShootTime = Time.timeSinceLevelLoad;
            shootBehaviour.Shoot();
            //audioSource.Play();
            ammoContainer.Dec();

            if (ammoContainer.count <= 0)
            {
                Reload();
            }
        }

        protected override bool CanShoot()
        {
            return Time.timeSinceLevelLoad > _lastShootTime + weaponData.shootRate  &&
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