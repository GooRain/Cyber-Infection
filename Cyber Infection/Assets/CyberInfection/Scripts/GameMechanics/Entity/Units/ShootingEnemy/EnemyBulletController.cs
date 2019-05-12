using UnityEngine;
using UnityEngine.Serialization;

namespace CyberInfection.GameMechanics.Entity.Enemy.ShootingEnemy
{
    public class EnemyBulletController : MonoBehaviour
    {
        [FormerlySerializedAs("speed")]
        [SerializeField]
        private float _speed = 5;
        [FormerlySerializedAs("lifeTime")]
        [SerializeField]
        private float _lifeTime = 1.2f;
        [FormerlySerializedAs("damage")]
        [SerializeField]
        private int _damage;
        private Collider2D m_Collider;

        void Update()
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
            Destroy(gameObject, _lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<IAlive>()?.GetDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
