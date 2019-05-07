using CyberInfection.Data.Entities.Unit.Enemy;
using UnityEngine;

namespace CyberInfection.GameMechanics.Entity.Units
{
    public class Enemy : Unit
    {
        [SerializeField] private EnemyData _data;
        
        protected override void Awake()
        {
            base.Awake();
            health = _data.health;

            UnitsManager.instance.OnEnemySpawn(this);
        }
    }
}
