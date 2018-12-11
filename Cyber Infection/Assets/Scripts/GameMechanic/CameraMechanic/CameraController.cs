using System.Collections;
using Data.Settings.Generation;
using GameMechanic.Unit.Player;
using Generation.Map;
using Persistent.Settings;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GameMechanic.CameraMechanic
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private float _dampTime = 0.2f;

		private Vector3 _velocity = Vector3.zero;
		private float _cameraZ;
		private Transform _playerTransform;
		private Camera _camera;

		// что то типа такого пока что, пока других размеров комнат нет.
		private Vector2 _maxXAndY = new Vector2(1.15f, 1.18f);
		private Vector2 _minXAndY = new Vector2(-0.4f, -0.15f);

		private Player _player;
		private MapSettingsData _mapSettingsData;

		[Inject]
		private void Construct(Player player, MapSettingsData map)
		{
			_player = player;
			_mapSettingsData = map;
		}

		private void Awake()
		{
			_camera = GetComponent<Camera>();
			_playerTransform = _player.transform;
			var minSize = _mapSettingsData.roomSizeInfo.roomWidth / 16f < _mapSettingsData.roomSizeInfo.roomHeight / 9f
				? _mapSettingsData.roomSizeInfo.roomWidth / 2f / 16f * 9f
				: _mapSettingsData.roomSizeInfo.roomHeight / 2f;
			_camera.orthographicSize = minSize;
		}

		private void Start()
		{
			_cameraZ = transform.position.z;
		}

		private void Update()
		{
			if (_playerTransform)
			{
				Vector3 delta = _playerTransform.transform.position -
				                _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _cameraZ));
				Vector3 destination = transform.position + delta;
				destination.z = _cameraZ;
				destination.x = Mathf.Clamp(delta.x, _minXAndY.x, _maxXAndY.x);
				destination.y = Mathf.Clamp(delta.y, _minXAndY.y, _maxXAndY.y);
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime);
			}
		}
	}
}