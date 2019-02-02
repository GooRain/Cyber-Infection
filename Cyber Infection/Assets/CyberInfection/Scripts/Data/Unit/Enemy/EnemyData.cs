using UnityEngine;

namespace CyberInfection.Data.Unit.Enemy
{
    [CreateAssetMenu(menuName = "Cyber Infection/Data/Units/Enemy", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField]
        private GameObject[] enemyPrefabs;

        public GameObject Instantiate(string type)
        {
            foreach (var prefab in enemyPrefabs)
            {
                if (prefab.name == type)
                {
                    var newObject = Instantiate(prefab);
                    newObject.name = type;
                    return newObject;
                }
            }

            return null;
        }

    }
}
