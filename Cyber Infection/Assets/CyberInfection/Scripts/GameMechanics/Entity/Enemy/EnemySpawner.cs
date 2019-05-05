using CyberInfection.Persistent;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Entity.Enemy
{
    public class EnemySpawner : SingletonMonobehaviour<EnemySpawner>
    {
        [SerializeField] private string _enemiesPrefabPrefix;
        [SerializeField] private int _maxEnemyID;

        private void Awake()
        {
            _instance = this;
        }

        public void SpawnEnemy(Vector3 pos)
        {
            PhotonNetwork.Instantiate(_enemiesPrefabPrefix + Random.Range(0, _maxEnemyID), pos, Quaternion.identity);
        }
    }
}