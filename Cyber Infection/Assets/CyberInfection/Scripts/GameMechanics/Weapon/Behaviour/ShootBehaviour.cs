using CyberInfection.Data.Entities;
using CyberInfection.GameMechanics.Projectile;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.Behaviour
{
    [System.Serializable]
    public class ShootBehaviour
    {
        public Transform muzzle;

        private BulletData m_BulletData;

        public ShootBehaviour(Transform muzzle, BulletData bulletData)
        {
            this.muzzle = muzzle;
            m_BulletData = bulletData;
        }

        public void Shoot()
        {
            var bullet = (Bullet) BulletPoolContainer.instance.Pop();

            if (UnityEngine.Camera.main != null)
            {
                var position = muzzle.position;
                Vector2 diff = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - position;
                bullet.cachedTransform.position = position;
                bullet.InitializeParameters(m_BulletData, diff.normalized);
            }
        }

        public bool CanShoot()
        {
            return true; // условия в будущем
        }
    }
}