using Boo.Lang;
using CyberInfection.Data.Entities;
using CyberInfection.GameMechanics.Projectile;
using CyberInfection.GameMechanics.Weapon.WeaponTypes;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponData pistolWeaponData; // test
        [SerializeField] private GameObject currentWeapon;
        [SerializeField] private Transform weaponHolder;

        private List<WeaponBase> m_WeaponList;
        private WeaponBase m_CurrentWeapon;

        private void Awake()
        {
            m_WeaponList = new List<WeaponBase> {currentWeapon.AddComponent<FireableWeapon>()};

            m_WeaponList[0].Initialize(pistolWeaponData, transform);

            if (m_WeaponList.Count > 0)
            {
                m_CurrentWeapon = m_WeaponList[0];
            }
        }

        public void SetMuzzlePos(Vector3 pos)
        {
        }
        
        public void Shoot()
        {
            m_CurrentWeapon.TryToShoot();
        }

        public void Reload()
        {
            m_CurrentWeapon.TryToReload();
        }
    }
}