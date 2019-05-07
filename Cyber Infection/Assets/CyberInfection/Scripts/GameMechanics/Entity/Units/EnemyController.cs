using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

namespace CyberInfection.GameMechanics.Entity.Units
{
    public class EnemyController : UnitController
    {
        [FormerlySerializedAs("followSpeed")]
        [SerializeField]
        private float _followSpeed = 1f;

        private Animator _animator;
        private Transform _target;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            
            enabled = false;

            StartCoroutine(SearchPlayer());
        }

        private IEnumerator SearchPlayer()
        {
            while (true)
            {
                if (_target == null)
                {
                    SetTarget(UnitsManager.instance.GetRandomPlayer().transform);
                }
                yield return new WaitForSeconds(0.5f);
            }
            // ReSharper disable once IteratorNeverReturns
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
            
            if (_animator != null && _animator.GetBool("iFollow"))
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _target.position, _followSpeed * Time.deltaTime);
            }
        }

        void StartFollowing()
        {
            if (_animator != null)
            {
                _animator.SetBool("iFollow", true);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_animator != null && collision.gameObject.tag.Equals("Player"))
            {
                _animator.SetBool("iFollow", false);
                // - HP
            }
        }
        
        public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_transform.position);
            }
            else
            {
                _transform.position = (Vector3) stream.ReceiveNext();
            }
        }
    }
}
