using System;
using CyberInfection.Data.Entities;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
    [System.Serializable]
    public class FireableWeapon : WeaponBase
    {
        public AudioSource audioSource;

        private WeaponSounds sounds;
        
        public override void Initialize(WeaponData data)
        {
            base.Initialize(data);
            sounds = data.sounds;
        }

        public override void Shoot(Vector2 direction)
        {
            lastShootTime = Time.timeSinceLevelLoad;
            
            shootBehaviour.Shoot(direction);
            
            audioSource.clip = sounds.fire;
            audioSource.Play();
            
            AmmoContainer.Dec();

            if (AmmoContainer.count <= 0)
            {
                Reload();
            }
        }

        public override bool CanShoot()
        {
            return Time.timeSinceLevelLoad > lastShootTime + weaponData.shootRate  &&
                   AmmoContainer.HasAny();
        }

        public override void Reload()
        {
            if (AmmoContainer.CanRestore())
            {
                AmmoContainer.Restore();
            }
        }
    }
}