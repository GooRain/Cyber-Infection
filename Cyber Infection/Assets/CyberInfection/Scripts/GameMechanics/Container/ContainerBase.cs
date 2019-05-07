using UnityEngine;

namespace CyberInfection.GameMechanics.Container
{
    [System.Serializable]
    public class ContainerBase
    {
        private int _capacity;
        private int _count;
        private int _totalStock;

        public int capacity
        {
            get { return _capacity; }
        }

        public int count
        {
            get { return _count; }
        }
        
        public ContainerBase(int capacity, int totalStock)
        {
            _capacity = capacity;
            _totalStock = totalStock;
            _count = _capacity;
        }

        private void SetCount(int value)
        {
            _count = Mathf.Clamp(value, 0, _capacity);
        }

        public void Dec()
        {
            SetCount(_count - 1);
        }

        public void Inc()
        {
            SetCount(_count + 1);
        }

        public void Restore()
        {
            _count = _capacity;
        }

        public bool CanRestore()
        {
            return _totalStock > 0;
        }

        public bool HasAny()
        {
            return _count > 0;
        }
    }
}