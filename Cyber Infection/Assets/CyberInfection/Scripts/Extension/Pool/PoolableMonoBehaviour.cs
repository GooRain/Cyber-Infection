using UnityEngine;

namespace CyberInfection.Extension.Pool
{
    public abstract class PoolableMonoBehaviour : MonoBehaviour, IPoolable
    {
        public Transform cachedTransform { get; private set; }
        public IPoolContainer poolContainer { get; protected set; }

        protected virtual void Awake()
        {
            cachedTransform = transform;
        }

        public void SetContainer(IPoolContainer poolContainer)
        {
            this.poolContainer = poolContainer;
        }
        
        public void Push()
        {
            poolContainer.Push(this);
        }

        public abstract void OnPush();
        public abstract void OnPop();
    }
}