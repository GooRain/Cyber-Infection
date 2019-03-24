using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
    [System.Serializable]
    public class FireableWeapon : WeaponBase
    {
        public AudioSource audioSource;

        public override void Shoot(Vector2 direction)
        {
            _lastShootTime = Time.timeSinceLevelLoad;
            
            shootBehaviour.Shoot(direction);
            //audioSource.Play();
            ammoContainer.Dec();

            if (ammoContainer.count <= 0)
            {
                Reload();
            }
        }

        public override bool CanShoot()
        {
            return Time.timeSinceLevelLoad > _lastShootTime + weaponData.shootRate  &&
                   ammoContainer.HasAny();
        }

        public override void Reload()
        {
            if (ammoContainer.CanRestore())
            {
                ammoContainer.Restore();
            }
        }
    }
}