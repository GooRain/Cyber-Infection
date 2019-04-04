using UnityEngine;
using UnityEngine.Serialization;

namespace CyberInfection.GameMechanics.Unit.Enemy.ShootingEnemy
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
        private float _damage;
        private Collider2D m_Collider;

        void Update()
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
            Destroy(gameObject, _lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<IAlive>()?.GetDamage(_damage);
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponent<IAlive>()?.GetDamage(_damage);
        }
    }
}
