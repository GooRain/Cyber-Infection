using UnityEngine;

namespace CyberInfection.Extension.Isometry
{
    [System.Serializable]
    public class IsometricObject
    {
        public Renderer renderer;
        public int offset;

        public IsometricObject(Renderer renderer)
        {
            this.renderer = renderer;
        }

        public void Sort(int order)
        {
            if (renderer == null)
            {
                return;
            }

            renderer.sortingOrder = order + offset;
        }
    }
}