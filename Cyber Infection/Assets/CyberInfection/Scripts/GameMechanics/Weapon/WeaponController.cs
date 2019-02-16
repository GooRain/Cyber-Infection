using Boo.Lang;
using CyberInfection.Data.Object;
using CyberInfection.GameMechanics.Weapon.WeaponTypes;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform muzzleTransform;
        [SerializeField] private WeaponData pistolWeaponData; // test

        private List<WeaponBase> _weaponList;
        private WeaponBase _currentWeapon;

        private void Awake()
        {
            _weaponList = new List<WeaponBase> {new Pistol(pistolWeaponData, muzzleTransform)};

            if (_weaponList.Count > 0)
            {
                _currentWeapon = _weaponList[0];
            }
        }

        public void SetMuzzlePos(Vector3 pos)
        {
            muzzleTransform.position = pos;
        }
        
        public void Shoot()
        {
            _currentWeapon.TryToShoot();
        }

        public void Reload()
        {
            _currentWeapon.TryToReload();
        }
    }
}