using UnityEngine;

namespace CyberInfection.Data.Entities
{
    [CreateAssetMenu( menuName = "Cyber Infection/Weapon/Bullet Data")]
    public class BulletData : ScriptableObject
    {
        public float speed;
        public int damage;
    }
}