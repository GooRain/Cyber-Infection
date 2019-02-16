using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.Behaviour
{
    [System.Serializable]
    public class ShootBehaviour
    {
        public Transform muzzle;
        public Bullet.Bullet bulletPrefab;

        public ShootBehaviour(Transform muzzle, Bullet.Bullet bulletPrefab)
        {
            this.muzzle = muzzle;
            this.bulletPrefab = bulletPrefab;
        }
        
        public void Shoot()
        {
            if (UnityEngine.Camera.main != null)
            {
                Vector2 diff = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - muzzle.position;
                float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                muzzle.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
            }
            
            Object.Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        }

        public bool CanShoot()
        {
            return true; // условия в будущем
        }
    }
}