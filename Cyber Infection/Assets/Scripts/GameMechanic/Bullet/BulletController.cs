using UnityEngine;
using UnityEngine.Serialization;

namespace GameMechanic.Bullet
{
	public class BulletController : MonoBehaviour
	{
		[FormerlySerializedAs("lifeTime")] [SerializeField]
		private float _lifeTime;
		[FormerlySerializedAs("bulletSpeed")] [SerializeField]
		private float _bulletSpeed = 7;

		private float _currentDistance = 0f;


		private void Update()
		{
			transform.Translate(Vector2.up * Time.deltaTime * _bulletSpeed);
            Destroy(gameObject,_lifeTime);
		}
	}
}
