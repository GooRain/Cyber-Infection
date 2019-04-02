using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.Extension.Pool
{
    [RequireComponent(typeof(PhotonView))]
    public class PoolContainer : MonoBehaviourPun, IPoolContainer
    {
        public class CachedRPC
        {
            public const string Push = "Push";
        }
        
        public string prefabPath;
        public int startCapacity = 10;

        protected readonly Stack<IPoolable> m_PoolStack = new Stack<IPoolable>();
        protected readonly List<IPoolable> m_PoolList = new List<IPoolable>();

        protected Transform m_Transform;

        private GameObject m_Prefab;

        public PhotonView cachedPhotonView => photonView;
        
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
                ExtendPool();
            }
        }

        public IPoolable Pop()
        {
            var obj = m_PoolStack.Count > 0 ? m_PoolStack.Pop() : ExtendPool();
            
            obj.cachedTransform.gameObject.SetActive(true);
            obj.OnPop();
            return obj;
        }

        [PunRPC]
        public void Push(int index)
        {
            if (index >= m_PoolList.Count)
            {
                return;
            }
            
            var obj = m_PoolList[index];
            obj.cachedTransform.gameObject.SetActive(false);
            obj.cachedTransform.SetParent(m_Transform);
            
            if (m_PoolStack.Contains(obj))
            {
                return;
            }
            
            m_PoolStack.Push(obj);
            obj.OnPush();
        }

        public void RpcPush(IPoolable obj)
        {
            if (PhotonNetwork.OfflineMode)
            {
                Push(obj.index);
                return;
            }
            
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC(CachedRPC.Push, RpcTarget.All, obj.index);
            }
        }

        private IPoolable ExtendPool()
        {
            var poolGameObject = Instantiate(m_Prefab, m_Transform, true);
            var poolObj = poolGameObject.GetComponent<IPoolable>();
            poolObj.index = m_PoolList.Count;
            poolObj.SetContainer(this);
            poolGameObject.SetActive(false);
            
            m_PoolList.Add(poolObj);
            m_PoolStack.Push(poolObj);
            
            return poolObj;
        }
    }
}