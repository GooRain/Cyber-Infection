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

        void Update()
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
            Destroy(gameObject, _lifeTime);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
