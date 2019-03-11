using CyberInfection.Extension.Pool;

namespace CyberInfection.GameMechanics.Projectile
{
    public class BulletPoolContainer : PoolContainer
    {
        public static BulletPoolContainer instance { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            
            InitializeContainerToPool();
        }

        private void InitializeContainerToPool()
        {
            foreach (var bullet in m_Store)
            {
                bullet.SetContainer(this);
            }

            instance = this;
        }
    }
}