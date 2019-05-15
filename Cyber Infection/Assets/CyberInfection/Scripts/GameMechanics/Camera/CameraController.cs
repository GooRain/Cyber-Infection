using CyberInfection.Data.Settings.Generation;
using CyberInfection.GameMechanics.Entity.Units;
using CyberInfection.Generation.Room;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CyberInfection.GameMechanics.Camera
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform anchor;
		[SerializeField] private float dampTime = 0.2f;
		[SerializeField] private Vector2 xOffset = new Vector2(-0.4f, 1.15f);
		[SerializeField] private Vector2 yOffset = new Vector2(-0.4f, 1.18f);

		[Header("Easings")] 
		
		[SerializeField] private float roomTransitionDuration = .5f;
		[SerializeField] private Ease roomTransitionEase = Ease.OutBack;

		public static CameraController Instance { get; private set; }
		
		private Vector3 velocity = Vector3.zero;
		private float cameraZ;
		private Transform playerTransform;
		private UnityEngine.Camera cam;
		private Transform cachedTransform;

		private Vector2 xBorder;
		private Vector2 yBorder;

		private RoomController previousRoom;

//		// что то типа такого пока что, пока других размеров комнат нет.
//		private Vector2 _maxXAndY = new Vector2(1.15f, 1.18f);
//		private Vector2 _minXAndY = new Vector2(-0.4f, -0.15f);

		private MapSettingsData mapSettingsData;

		[Inject]
		private void Construct(MapSettingsData map)
		{
			mapSettingsData = map;
		}

		public void SetPlayer(Player player)
		{
			playerTransform = player.transform;
		}

		private void Awake()
		{
			Instance = this;

			cam = GetComponent<UnityEngine.Camera>();
			cachedTransform = transform;
			var minSize = mapSettingsData.roomSizeInfo.roomWidth / 16f < mapSettingsData.roomSizeInfo.roomHeight / 9f
				? (mapSettingsData.roomSizeInfo.roomWidth - 2) * .5f / 16f * 9f
				: (mapSettingsData.roomSizeInfo.roomHeight - 2) * .5f; // Vremenno, potom nado sohranyat sootnoshenie ekrana
			cam.orthographicSize = minSize;

			//CalculateBorders();
		}

		private void Start()
		{
			cameraZ = transform.position.z;
		}

		private void LateUpdate()
		{
			FollowPlayer();
		}

		public void SetRoom(RoomController roomController)
		{
			//_anchor.position = roomController.transform.position;
			anchor.DOKill();
			anchor.DOMove(roomController.transform.position, roomTransitionDuration).SetEase(roomTransitionEase);

			if (previousRoom != null)
			{
				previousRoom.OnUnFocus();
			}
			
			roomController.OnFocus();
			
			previousRoom = roomController;

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
			if (!playerTransform) return;

//			var delta = _playerTransform.position -
//			            _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _cameraZ));
			var delta = playerTransform.position - anchor.position;
			var position = cachedTransform.localPosition;
			var destination = position + delta;
			destination.z = cameraZ;
			destination.x = Mathf.Clamp(delta.x, xOffset.x, xOffset.y);
			destination.y = Mathf.Clamp(delta.y, yOffset.x, yOffset.y);
			position = Vector3.SmoothDamp(position, destination, ref velocity, dampTime);
			cachedTransform.localPosition = position;
		}
	}
}