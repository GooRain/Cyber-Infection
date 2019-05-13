using UnityEngine;

namespace CyberInfection.Extension
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    public class IsometricObject : MonoBehaviour
    {
        private const int IsometricRangePerYUnit = 100;
   
        [Tooltip("Will use this object to compute z-order")]
        public Transform Target;
    
        [Tooltip("Use this to offset the object slightly in front or behind the Target object")]
        public int TargetOffset = 0;

        private Renderer _renderer;
        
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        void Update()
        {
            if (Target == null)
            {
                Target = transform;
            }
            
            _renderer.sortingOrder = -(int)(Target.position.y * IsometricRangePerYUnit) + TargetOffset;
        }
    }
}