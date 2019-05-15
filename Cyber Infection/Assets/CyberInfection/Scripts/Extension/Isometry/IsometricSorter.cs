using System.Collections.Generic;
using UnityEngine;

namespace CyberInfection.Extension.Isometry
{
    public class IsometricSorter : MonoBehaviour
    {
        private const int IsometricRangePerYUnit = 100;

        public Transform target;
        public List<IsometricObject> isometricObjects;

        private void Reset()
        {
            target = transform;
            var renderers = GetComponentsInChildren<Renderer>();

            foreach (var r in renderers)
            {
                isometricObjects.Add(new IsometricObject(r));
            }
        }

        private void Update()
        {
            var order = -(int) (target.position.y * IsometricRangePerYUnit);
            foreach (var r in isometricObjects)
            {
                r.Sort(order);
            }
        }
    }
}