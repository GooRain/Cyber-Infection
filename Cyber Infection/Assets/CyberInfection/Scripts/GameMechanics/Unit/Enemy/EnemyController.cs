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

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
            _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }
        void Start()
        {
            
        }

        
        void Update()
        {
            EnemyFollow();
        }

        void EnemyFollow()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0)) // На самом деле тут должно быть что то вроде когда все загрузится и гг будет готов убивать.
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
    }
}
