using UnityEngine;
using UnityEngine.Serialization;

namespace CyberInfection.GameMechanics.Unit.Enemy
{
    public class EnemyController : UnitController
    {
        [FormerlySerializedAs("followSpeed")]
        [SerializeField]
        private float _followSpeed = 1f;

        private Animator _animator;
        private Transform _playerPos;
        private bool _inited;

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            if (!_inited)
            {
                if (GameObject.FindGameObjectWithTag("Player"))
                {
                    _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
                    _inited = true;
                }
                return;
            }
            EnemyFollow();
        }

        void EnemyFollow()
        {
            if (Vector2.Distance(transform.position, _playerPos.position) > 1)
            {
                StartFollowing();
            }
            if (_animator.GetBool("iFollow"))
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _playerPos.position, _followSpeed * Time.deltaTime);
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
                Debug.Log("-5HP");
            }
        }
    }
}
