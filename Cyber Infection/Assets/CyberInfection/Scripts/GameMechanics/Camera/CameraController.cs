using CyberInfection.Data.Settings.Generation;
using CyberInfection.GameMechanics.Entity.Player;
using CyberInfection.Generation.Room;
using CyberInfection.Persistent;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CyberInfection.GameMechanics.Camera
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform _anchor;
		[SerializeField] private float _dampTime = 0.2f;
		[SerializeField] private Vector2 _xOffset = new Vector2(-0.4f, 1.15f);
		[SerializeField] private Vector2 _yOffset = new Vector2(-0.4f, 1.18f);

		[Header("Easings")] 
		
		[SerializeField] private float _roomTransitionDuration = .5f;
		[SerializeField] private Ease _roomTransitionEase = Ease.OutBack;

		public static CameraController instance { get; private set; }
		
		private Vector3 _velocity = Vector3.zero;
		private float _cameraZ;
		private Transform _playerTransform;
		private UnityEngine.Camera _camera;
		private Transform _transform;

		private Vector2 _xBorder;
		private Vector2 _yBorder;

//		// что то типа такого пока что, пока других размеров комнат нет.
//		private Vector2 _maxXAndY = new Vector2(1.15f, 1.18f);
//		private Vector2 _minXAndY = new Vector2(-0.4f, -0.15f);

		private MapSettingsData _mapSettingsData;

		[Inject]
		private void Construct(MapSettingsData map)
		{
			_mapSettingsData = map;
		}

		public void SetPlayer(Player player)
		{
			_playerTransform = player.transform;
		}

		private void Awake()
		{
			instance = this;

			_camera = GetComponent<UnityEngine.Camera>();
			_transform = transform;
			var minSize = _mapSettingsData.roomSizeInfo.roomWidth / 16f < _mapSettingsData.roomSizeInfo.roomHeight / 9f
				? (_mapSettingsData.roomSizeInfo.roomWidth - 2) * .5f / 16f * 9f
				: (_mapSettingsData.roomSizeInfo.roomHeight - 2) * .5f; // Vremenno, potom nado sohranyat sootnoshenie ekrana
			_camera.orthographicSize = minSize;

			//CalculateBorders();
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
			//_anchor.position = roomController.transform.position;

			_anchor.DOMove(roomController.transform.position, _roomTransitionDuration).SetEase(_roomTransitionEase);

			//CalculateBorders();
		}

//		private void CalculateBorders()
//		{
//			_xBorder.x = _anchor.x - _xOffset.x;
//			_xBorder.y = _anchor.x + _xOffset.y;
//
//			Debug.Log(_xBorder);
//			_yBorder.x = _anchor.y - _yOffset.x;
//			_yBorder.y = _anchor.y + _yOffset.y;
//
//			Debug.Log(_yBorder);
//		}

		private void FollowPlayer()
		{
			if (!_playerTransform) return;

//			var delta = _playerTransform.position -
//			            _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _cameraZ));
			var delta = _playerTransform.position - _anchor.position;
			var position = _transform.localPosition;
			var destination = position + delta;
			destination.z = _cameraZ;
			destination.x = Mathf.Clamp(delta.x, _xOffset.x, _xOffset.y);
			destination.y = Mathf.Clamp(delta.y, _yOffset.x, _yOffset.y);
			position = Vector3.SmoothDamp(position, destination, ref _velocity, _dampTime);
			_transform.localPosition = position;
		}
	}
}