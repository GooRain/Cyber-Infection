using System;
using System.Collections.Generic;
using CyberInfection.Persistent;
using UnityEngine.SceneManagement;

namespace CyberInfection.GameMechanics.Entity
{
    public class UnitsManager : SingletonMonobehaviour<UnitsManager>
    {
        private List<Player.Player> players;
        private List<Unit> units;
        private List<Enemy.Enemy> enemies;

        public event Action<Player.Player> onPlayerSpawn;

        private void Awake()
        {
            _instance = this;
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            
            Initialize();
        }

        private void SceneManagerOnActiveSceneChanged(Scene from, Scene to)
        {
            Initialize();
        }

        private void Initialize()
        {
            players = new List<Player.Player>();
            units = new List<Unit>();
            enemies = new List<Enemy.Enemy>();
            onPlayerSpawn = delegate { };
        }

        public void OnPlayerSpawn(Player.Player player)
        {
            players.Add(player);
            onPlayerSpawn?.Invoke(player);
        }
        
        public void OnPlayerDeath(Player.Player player)
        {
            players.Remove(player);
        }

        public void OnUnitSpawn(Unit unit)
        {
            units.Add(unit);
        }

        public void OnUnitDeath(Unit unit)
        {
            units.Remove(unit);
        }
        
        public void OnEnemySpawn(Enemy.Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void OnEnemyDeath(Enemy.Enemy enemy)
        {
            enemies.Remove(enemy);
        }
    }
}