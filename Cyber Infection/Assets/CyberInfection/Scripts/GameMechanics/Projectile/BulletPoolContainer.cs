using CyberInfection.Extension.Pool;

namespace CyberInfection.GameMechanics.Projectile
{
    public class BulletPoolContainer : PoolContainer
    {
        public static BulletPoolContainer instance { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            
            instance = this;
        }
    }
}