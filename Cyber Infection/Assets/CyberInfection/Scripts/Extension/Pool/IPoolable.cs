using UnityEngine;
namespace CyberInfection.Extension.Pool
{
    public interface IPoolable
    {
        Transform cachedTransform { get; }

        void Push();
        void OnPush();
        void OnPop();
        void SetContainer(IPoolContainer poolContainer);
    }
}