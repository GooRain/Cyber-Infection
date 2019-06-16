using System;
using System.Collections;
using CyberInfection.Data.Entities;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
    [System.Serializable]
    public class FireableWeapon : WeaponBase
    {
        public AudioSource audioSource;
        public GameObject muzzleFlash;
        public float muzzleFlashTime = 0.05f;

        private WeaponSounds sounds;
        private IEnumerator muzzleFlashCoroutine;
        
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

            ShowMuzzle();
            
            AmmoContainer.Dec();

            if (AmmoContainer.count <= 0)
            {
                Reload();
            }
        }

        private void ShowMuzzle()
        {
            muzzleFlashCoroutine = ShowMuzzleForTime(muzzleFlashTime);
            StartCoroutine(muzzleFlashCoroutine);
        }

        private IEnumerator ShowMuzzleForTime(float time)
        {
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(time);
            muzzleFlash.SetActive(false);
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