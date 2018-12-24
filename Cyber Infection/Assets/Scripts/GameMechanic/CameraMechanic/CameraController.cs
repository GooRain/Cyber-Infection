using System.Collections;
using Data.Settings.Generation;
using Extension;
using GameMechanic.Unit.Player;
using Generation.Map;
using Generation.Room;
using Persistent;
using Persistent.Settings;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GameMechanic.CameraMechanic
{
	public class CameraController : SingletonMonobehaviour<CameraController>
	{
		[SerializeField] private float _dampTime = 0.2f;
		[SerializeField] private Vector2 _xOffset = new Vector2(-0.4f, 1.15f);
		[SerializeField] private Vector2 _yOffset = new Vector2(-0.15f, 1.18f);

		private Vector3 _velocity = Vector3.zero;
		private float _cameraZ;
		private Transform _playerTransform;
		private Camera _camera;
		private Transform _transform;

		private Vector2 _xBorder;
		private Vector2 _yBorder;

//		// что то типа такого пока что, пока других размеров комнат нет.
//		private Vector2 _maxXAndY = new Vector2(1.15f, 1.18f);
//		private Vector2 _minXAndY = new Vector2(-0.4f, -0.15f);

		private Player _player;
		private MapSettingsData _mapSettingsData;

		private Vector2 _anchor;

		[Inject]
		private void Construct(Player player, MapSettingsData map)
		{
			_player = player;
			_mapSettingsData = map;
		}

		private void Awake()
		{
			_instance = this;
			
			_camera = GetComponent<Camera>();
			_transform = transform;
			_playerTransform = _player.transform;
			var minSize = _mapSettingsData.roomSizeInfo.roomWidth / 16f < _mapSettingsData.roomSizeInfo.roomHeight / 9f
				? _mapSettingsData.roomSizeInfo.roomWidth / 2f / 16f * 9f
				: _mapSettingsData.roomSizeInfo.roomHeight / 2f; // Vremenno, potom nado sohranyat sootnoshenie ekrana
			_camera.orthographicSize = minSize;

			_anchor = _transform.position;
			CalculateBorders();
		}

		private void Start()
		{
			_cameraZ = transform.position.z;
		}

		private void LateUpdate()
		{
			FollowPlayer();
		}

		public void SetRoom(RoomController roomController)
		{
			_anchor = roomController.transform.position;

			CalculateBorders();
		}

		private void CalculateBorders()
		{
			_xBorder.x = _anchor.x + _xOffset.x;
			_xBorder.y = _anchor.x + _xOffset.y;
			
			_yBorder.x = _anchor.y + _yOffset.x;
			_yBorder.y = _anchor.y + _yOffset.y;
		}

		private void FollowPlayer()
		{
			if (!_playerTransform) return;

			var delta = _playerTransform.position -
			            _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _cameraZ));
			var position = _transform.position;
			var destination = position + delta;
			destination.z = _cameraZ;
			destination.x = Mathf.Clamp(delta.x, _xBorder.x, _xBorder.y);
			destination.y = Mathf.Clamp(delta.y, _yBorder.x, _yBorder.y);
			position = Vector3.SmoothDamp(position, destination, ref _velocity, _dampTime);
			_transform.position = position;
		}
	}
}