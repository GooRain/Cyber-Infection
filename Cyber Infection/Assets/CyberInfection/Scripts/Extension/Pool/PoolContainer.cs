using System.Collections.Generic;
using UnityEngine;

namespace CyberInfection.Extension.Pool
{
    public class PoolContainer : MonoBehaviour, IPoolContainer
    {
        public string prefabPath;
        public int startCapacity = 10;

        protected readonly Stack<IPoolable> m_Store = new Stack<IPoolable>();

        protected Transform m_Transform;

        private GameObject m_Prefab;

        protected virtual void Awake()
        {
            if (!LoadPrefab())
            {
                return;
            }
            
            m_Transform = transform;
            InitializePool(startCapacity);
        }

        private bool LoadPrefab()
        {
            m_Prefab = Resources.Load<GameObject>(prefabPath);

            if (m_Prefab == null)
            {
                Debug.LogError("Couldn't load prefab at path: " + prefabPath);
                return false;
            }

            if (m_Prefab.GetComponent<IPoolable>() == null)
            {
                Debug.LogError("Prefab must contain component that implements IPoolable!");
                return false;
            }

            return true;
        }
        
        protected void InitializePool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Push(ExtendPool());
            }
        }

        public IPoolable Pop()
        {
            var obj = m_Store.Count > 0 ? m_Store.Pop() : ExtendPool();

            obj.cachedTransform.gameObject.SetActive(true);
            obj.OnPop();
            return obj;
        }

        public void Push(IPoolable obj) 
        {
            obj.cachedTransform.gameObject.SetActive(false);
            obj.cachedTransform.SetParent(m_Transform);
            if (!m_Store.Contains(obj))
            {
                m_Store.Push(obj);
                obj.OnPush();
            }
        }

        private IPoolable ExtendPool()
        {
            return Instantiate(m_Prefab).GetComponent<IPoolable>();
        }
    }
}