using CyberInfection.Data.Entities;
using CyberInfection.GameMechanics.Projectile;
using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.Behaviour
{
    [System.Serializable]
    public class ShootBehaviour
    {
        public Transform muzzle { get; }

        private BulletData m_BulletData;

        public ShootBehaviour(Transform muzzle, BulletData bulletData)
        {
            this.muzzle = muzzle;
            m_BulletData = bulletData;
        }

        public void Shoot(Vector2 direction)
        {
            var bullet = (Bullet) BulletPoolContainer.instance.Pop();

            if (UnityEngine.Camera.main != null)
            {
                bullet.cachedTransform.position = muzzle.position;
                bullet.InitializeParameters(m_BulletData, direction);
            }
        }

        public bool CanShoot()
        {
            return true; // условия в будущем
        }
    }
}