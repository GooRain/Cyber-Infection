using CyberInfection.Data.Unit.Enemy;
using UnityEngine;

namespace CyberInfection.GameMechanics.Unit.Enemy
{
    public class Enemy : Unit
    {
        [SerializeField] private EnemyData _data;
        
        protected override void Awake()
        {
            base.Awake();
            health = _data.health;
        }
    }
}
