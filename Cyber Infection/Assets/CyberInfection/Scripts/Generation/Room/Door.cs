using System;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
    public class Door : MonoBehaviour
    {
        public event Action<Collider2D> onTriggerEnterEvent = delegate { };
        public event Action<Collision2D> onCollisionEnterEvent= delegate { };
        
        public void Toggle(bool value)
        {
            gameObject.SetActive(value);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            onTriggerEnterEvent.Invoke(other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            onCollisionEnterEvent.Invoke(other);
        }
    }
}