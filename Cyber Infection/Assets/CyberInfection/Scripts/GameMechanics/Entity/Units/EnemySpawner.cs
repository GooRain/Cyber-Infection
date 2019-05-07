using System.Collections.Generic;
using CyberInfection.Data.Settings.Generation;
using CyberInfection.Extension.Enums;
using CyberInfection.Persistent;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Entity.Units
{
    public class EnemySpawner : SingletonMonobehaviour<EnemySpawner>
    {
        [SerializeField] private EnemySpawnData _spawnData;
        [SerializeField] private string _enemiesPrefabPrefix;
        [SerializeField] private List<EnemyDifficulty> _availableDifficulties;

        private void Awake()
        {
            _instance = this;
        }

        public void SpawnEnemy(Vector3 pos)
        {
            SpawnEnemy(_availableDifficulties[Random.Range(0, _availableDifficulties.Count)], pos);
        }

        public void SpawnEnemy(EnemyDifficulty difficulty, Vector3 pos)
        {
            var path = _enemiesPrefabPrefix + _spawnData.enemiesDictionary[difficulty].GetRandomPrefab().name;
            Debug.Log("Difficulty = " + difficulty + " ::: Enemy Prefab Path = " + path);
            PhotonNetwork.Instantiate(path, pos, Quaternion.identity);
        }
    }
}