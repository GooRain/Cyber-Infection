using UnityEngine;
using UnityEngine.Serialization;

namespace CyberInfection.GameMechanics.Entity.Enemy
{
    public class EnemyController : UnitController
    {
        [FormerlySerializedAs("followSpeed")]
        [SerializeField]
        private float _followSpeed = 1f;

        private Animator _animator;
        private Transform _target;

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
            
            enabled = false;
        }

        private void SetTarget(Transform newTarget)
        {
            _target = newTarget;
            enabled = true;
        }

        private void Update()
        {
            EnemyFollow();
        }

        void EnemyFollow()
        {
            if (_target == null)
            {
                enabled = false;
                return;
            }

            if (Vector2.Distance(transform.position, _target.position) > 1)
            {
                StartFollowing();
            }
            if (_animator.GetBool("iFollow"))
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _target.position, _followSpeed * Time.deltaTime);
            }
        }

        void StartFollowing()
        {
            _animator.SetBool("iFollow", true);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                _animator.SetBool("iFollow", false);
                // - HP
            }
        }
    }
}
