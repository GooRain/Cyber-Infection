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

        private Transform _target;

        private Transform _transform;

        public bool isFollowing { get; set; }

        private void Awake()
        {
            _transform = transform;
            
            enabled = false;

            StartCoroutine(SearchPlayer());
        }

        private IEnumerator SearchPlayer()
        {
            while (true)
            {
                if (_target == null)
                {
                    var player = UnitsManager.instance.GetRandomPlayer();
                    if (player != null)
                    {
                        SetTarget(player.transform);
                    }
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
                StopFollowing();
                enabled = false;
                return;
            }

            if (Vector2.Distance(transform.position, _target.position) > 1)
            {
                StartFollowing();
            }
            else
            {
                StopFollowing();
            }
            
            if (isFollowing)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _target.position, _followSpeed * Time.deltaTime);
            }
        }

        private void StopFollowing()
        {
            isFollowing = false;
        }

        private void StartFollowing()
        {
            isFollowing = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                collision.gameObject.GetComponent<Player>().GetDamage(10);
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
