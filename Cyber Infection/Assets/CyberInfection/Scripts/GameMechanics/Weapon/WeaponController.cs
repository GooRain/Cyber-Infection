using Boo.Lang;
using CyberInfection.Data.Entities;
using CyberInfection.Extension;
using CyberInfection.GameMechanics.Projectile;
using CyberInfection.GameMechanics.Unit;
using CyberInfection.GameMechanics.Weapon.WeaponTypes;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon
{
    [RequireComponent(typeof(IUnit))]
    public class WeaponController : MonoBehaviour
    {
        public class CachedRPC
        {
            public const string Shoot = "RPCShoot";
        }

        [SerializeField] private WeaponData pistolWeaponData; // test
        [SerializeField] private GameObject currentWeapon;
        [SerializeField] private Transform weaponHolder;

        private List<WeaponBase> m_WeaponList;
        private WeaponBase m_CurrentWeapon;
        private UnityEngine.Camera m_Camera;

        private IUnit m_Unit;

        private void Awake()
        {
            m_Unit = GetComponent<IUnit>();
            m_WeaponList = new List<WeaponBase> {currentWeapon.GetComponent<FireableWeapon>()};
            m_Camera = UnityEngine.Camera.main;

            m_WeaponList[0].Initialize(pistolWeaponData);

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
            var direction = (m_Camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).OnlyXY() - 
                        m_CurrentWeapon.muzzle.position.OnlyXY()).normalized;

            if (m_CurrentWeapon.CanShoot())
            {
                m_Unit.cachedPhotonView.RPC(CachedRPC.Shoot, RpcTarget.All, direction);
            }
        }

        
        [PunRPC]
        public void RPCShoot(Vector2 direction)
        {
            m_CurrentWeapon.Shoot(direction);
        }
        
        public void Reload()
        {
            m_CurrentWeapon.TryToReload();
        }
    }
}