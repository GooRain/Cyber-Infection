using CyberInfection.Extension.Utility;
using UnityEngine;

namespace CyberInfection.Data.Settings.Generation
{
    [CreateAssetMenu(menuName = "Cyber Infection/Data/Enemy Spawn Data")]
    public class EnemySpawnData : ScriptableObject
    {
        [SerializeField] private EnemyPrefabsDictionary _enemiesDictionary;
        
        public EnemyPrefabsDictionary enemiesDictionary => _enemiesDictionary;
    }
}