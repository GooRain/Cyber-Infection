using System.Collections.Generic;
using UnityEngine;

namespace CyberInfection.Extension.Pool
{
    public class GenericPoolContainer<T> : MonoBehaviour where T : MonoBehaviour, IPoolable
    {
        public T prefab;
        public int startCapacity = 10;

        protected readonly Stack<T> m_Store = new Stack<T>();

        protected Transform m_Transform;

        protected virtual void Awake()
        {
            m_Transform = transform;
            InitializePool(startCapacity);
        }

        protected void InitializePool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Push(ExtendPool());
            }
        }

        public T Pop()
        {
            var obj = m_Store.Count > 0 ? m_Store.Pop() : ExtendPool();

            obj.gameObject.SetActive(true);
            obj.OnPop();
            return obj;
        }

        public void Push(T obj) 
        {
            obj.gameObject.SetActive(false);
            obj.cachedTransform.SetParent(m_Transform);
            if (!m_Store.Contains(obj))
            {
                m_Store.Push(obj);
                obj.OnPush();
            }
        }

        private T ExtendPool()
        {
            var go = Instantiate(prefab);
            var obj = go.GetComponent<T>();
            return obj;
        }
    }
}