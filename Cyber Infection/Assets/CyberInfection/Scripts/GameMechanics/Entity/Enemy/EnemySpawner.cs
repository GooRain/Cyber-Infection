using CyberInfection.Data.Settings.Generation;
using CyberInfection.Extension.Enums;
using CyberInfection.Persistent;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Entity.Enemy
{
    public class EnemySpawner : SingletonMonobehaviour<EnemySpawner>
    {
        [SerializeField] private EnemySpawnData _spawnData;
        [SerializeField] private string _enemiesPrefabPrefix;

        private void Awake()
        {
            _instance = this;
        }
        
        public void SpawnEnemy(EnemyDifficulty difficulty, Vector3 pos)
        {
            var path = _enemiesPrefabPrefix + _spawnData.enemiesDictionary[difficulty].GetRandomPrefab().name;
            PhotonNetwork.Instantiate(path, pos, Quaternion.identity);
        }
    }
}