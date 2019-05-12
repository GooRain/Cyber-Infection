using UnityEngine;
using UnityEngine.Serialization;

namespace CyberInfection.GameMechanics.Unit.Enemy.ShootingEnemy
{
    public class ShootingEnemy : UnitController
    {
        [FormerlySerializedAs("speed")]
        [SerializeField]
        private float _speed;
        [FormerlySerializedAs("stopDistance")]
        [SerializeField]
        private float _stopDistance;
        [FormerlySerializedAs("retreatDistance")]
        [SerializeField]
        private float _retreatDistance;
        [FormerlySerializedAs("reloadSpeed")]
        [SerializeField]
        private float _reloadSpeed;
        private float _timeBtwShots;
        [FormerlySerializedAs("projectile")]
        [SerializeField]
        private GameObject projectile;
        [FormerlySerializedAs("enemytShotPos")]
        [SerializeField]
        private GameObject _enemyShotPos;

        private AudioSource audioS;
        private Transform _player;
        private Vector2 _target;

        private bool _inited;

        void Start()
        {
            //_player = GameObject.FindGameObjectWithTag("Player").transform;
            _timeBtwShots = _reloadSpeed;
            audioS = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (!_inited)
            {
                if (GameObject.FindGameObjectWithTag("Player"))
                {
                    _player = GameObject.FindGameObjectWithTag("Player").transform;
                    _inited = true;
                }
                return;
            }
            SmartMovement();
            Shooting();
        }

        void Shooting()
        {
            //Where player
            _target = new Vector2(_player.position.x, _player.position.y);
            //For Good Vector
            Vector2 diff = _target - new Vector2(_enemyShotPos.transform.position.x, _enemyShotPos.transform.position.y);
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            _enemyShotPos.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
            if (_timeBtwShots <= 0)
            {
                Instantiate(projectile, _enemyShotPos.transform.position, _enemyShotPos.transform.rotation);
                _timeBtwShots = _reloadSpeed;
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }
        }

        void SmartMovement()
        {
            if (Vector2.Distance(transform.position, _player.position) > _stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, _player.position) < _stopDistance && Vector2.Distance(transform.position, _player.position) > _retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, _player.position) < _retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, -_speed * Time.deltaTime);
            }
        }
    }
}
