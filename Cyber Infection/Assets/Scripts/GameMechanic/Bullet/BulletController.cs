using UnityEngine;
using UnityEngine.Serialization;

namespace GameMechanic.Bullet
{
	public class BulletController : MonoBehaviour
	{
		[FormerlySerializedAs("maxDistance")] [SerializeField]
		private float _maxDistance;
		[FormerlySerializedAs("bulletSpeed")] [SerializeField]
		private float _bulletSpeed = 7;

		private Vector2 _startPos;
		private float _currentDistance = 0f;

		private void Start()
		{
			_startPos = transform.position;
		}

		private void Update()
		{
			transform.Translate(Vector2.up * Time.deltaTime * _bulletSpeed);          // ВПЕРЕД ЛЕТИТ И ТОЛЬКО ВПЕРЕД
			_currentDistance = Vector2.Distance(transform.position, _startPos);                                    // Дистанцию чекает

			if(_currentDistance >= _maxDistance)
				Destroy(gameObject);
		}
	}
}
