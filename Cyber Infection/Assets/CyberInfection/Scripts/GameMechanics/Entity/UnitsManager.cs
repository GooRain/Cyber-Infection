using System;
using System.Collections.Generic;
using CyberInfection.GameMechanics.Entity.Units;
using CyberInfection.Persistent;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace CyberInfection.GameMechanics.Entity
{
    public class UnitsManager : SingletonMonobehaviour<UnitsManager>
    {
        private List<Player> _players;
        private List<Unit> _units;

        private List<Units.Enemy> _enemies;

        public event Action<Player> onPlayerSpawn;

        public List<Player> players => _players;
        public List<Unit> units => _units;
        public List<Units.Enemy> enemies => _enemies;
        
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
            _players = new List<Player>();
            _units = new List<Unit>();
            _enemies = new List<Units.Enemy>();
            onPlayerSpawn = delegate { };
        }

        public void OnPlayerSpawn(Player player)
        {
            _players.Add(player);
            onPlayerSpawn?.Invoke(player);
        }
        
        public void OnPlayerDeath(Player player)
        {
            _players.Remove(player);
        }

        public void OnUnitSpawn(Unit unit)
        {
            _units.Add(unit);
        }

        public void OnUnitDeath(Unit unit)
        {
            _units.Remove(unit);
        }
        
        public void OnEnemySpawn(Units.Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void OnEnemyDeath(Units.Enemy enemy)
        {
            _enemies.Remove(enemy);
        }

        public Player GetRandomPlayer()
        {
            return _players[Random.Range(0, _players.Count)];
        }
    }
}