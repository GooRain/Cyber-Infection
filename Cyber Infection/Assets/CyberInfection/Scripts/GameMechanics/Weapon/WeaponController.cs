using Boo.Lang;
using CyberInfection.Data.Object;
using CyberInfection.GameMechanics.Weapon.WeaponTypes;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponData pistolWeaponData; // test
        [SerializeField] private GameObject currentWeapon;
        [SerializeField] private Transform weaponHolder;

        private List<WeaponBase> _weaponList;
        private WeaponBase _currentWeapon;

        private void Awake()
        {
            _weaponList = new List<WeaponBase> {currentWeapon.AddComponent<FireableWeapon>()};

            _weaponList[0].Initialize(pistolWeaponData, transform);

            if (_weaponList.Count > 0)
            {
                _currentWeapon = _weaponList[0];
            }
        }

        public void SetMuzzlePos(Vector3 pos)
        {
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