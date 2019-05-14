using System;
using CyberInfection.Data.Entities.Unit.Enemy;
using UnityEngine;

namespace CyberInfection.GameMechanics.Entity.Units
{
    public class Enemy : Unit
    {
        [SerializeField] private EnemyData _data;

        public event Action<Enemy> OnDeath = delegate { };

        protected override void Awake()
        {
            base.Awake();
            health = _data.health;

            UnitsManager.instance.OnEnemySpawn(this);
        }

        public override void Die()
        {
            OnDeath.Invoke(this);
            
            base.Die();
        }
    }
}
