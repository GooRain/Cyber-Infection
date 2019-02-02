using System.Collections;
using CyberInfection.Data.Unit.Enemy;
using UnityEngine;

namespace CyberInfection.GameMechanics.Spawn
{
    public class EnemySpawn : MonoBehaviour
    {
        public EnemyData Pool { get; set; }

        void Start()
        {
            StartCoroutine(SpawnControl());
        }

        private void Awake()
        {
            Pool = GetComponent<EnemyData>();
        }

        private IEnumerator SpawnControl()
        {
            int enemyIndex = Random.Range(0, 4);
            string type = string.Empty;
            switch (enemyIndex)
            {
                case 0:
                    type = "blue_enemy";
                    break;
                case 1:
                    type = "orange_enemy";
                    break;
                case 2:
                    type = "purple_enemy";
                    break;
                case 3:
                    type = "red_enemy";
                    break;
            }

            Pool.Instantiate(type);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
