using UnityEngine;
namespace CyberInfection.Extension.Pool
{
    public interface IPoolable
    {
        Transform cachedTransform { get; }
        
        int index { get; set; }

        void Push();
        void OnPush();
        void OnPop();
        void SetContainer(IPoolContainer poolContainer);
    }
}