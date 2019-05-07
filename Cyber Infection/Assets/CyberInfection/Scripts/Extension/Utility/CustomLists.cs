using System.Collections.Generic;
using UnityEngine;

namespace CyberInfection.Extension.Utility
{
    [System.Serializable]
    public class EnemiesPrefabs
    {
        [SerializeField] private List<GameObject> _list;

        public GameObject GetRandomPrefab()
        {
            return NullCondition() ? null : _list[Random.Range(0, _list.Count)];
        }

        public GameObject GetPrefabAt(int index)
        {
            return NullCondition() ? null : _list[index];
        }

        private bool NullCondition()
        {
            return _list.Count == 0;
        }
    }
}