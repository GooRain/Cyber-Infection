using System;
using GameMechanic.Unit.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameMechanic.Unit.Player
{
	public class PlayerController : UnitController
	{
		[FormerlySerializedAs("walkSpeed")] [SerializeField]
		private float _walkSpeed;

		[FormerlySerializedAs("runSpeed")] [SerializeField]
		private float _runSpeed;

		[FormerlySerializedAs("reloadSpeed")] [SerializeField]
		private float _reloadSpeed;

		[SerializeField] private Sprite[] differentRotation;
		//[SerializeField]
		//private GameObject bulletSpawnPoint;
		//[SerializeField]
		//private GameObject bullet;            // пуля которая будет стрелять

		private SpriteRenderer _sprite;

		private Animator _animator;
		//private CharState State
		//{
		//	get { return (CharState)animator.GetInteger("State"); }
		//	set { animator.SetInteger("State", (int)value); }
		//}

		private float _waitTime;
		private bool _attacking;

		private Vector3 _movement; // The vector to store the direction of the player's movement.
		private Rigidbody _playerRigidbody; // Reference to the player's rigidbody.
		private int _floorMask; // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
		private float _camRayLength = 100f; // The length of the ray from the camera into the scene.
		private SpriteRenderer _spriteRenderer;
		private Camera _camera;
		private Transform _transform;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_sprite = GetComponentInChildren<SpriteRenderer>();
			_playerRigidbody = GetComponent<Rigidbody>();
			_floorMask = LayerMask.GetMask("Floor");
			_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			_camera = Camera.main;
			_transform = transform;
		}

		private void Start()
		{
		}

		private void Update()
		{
			//if(!attacking) State = CharState.idle;
			Movement();
			Shoot();
			RotationMouse();
		}

		private void FixedUpdate()
		{
			//float h = Input.GetAxisRaw("Horizontal");
			//float v = Input.GetAxisRaw("Vertical");

			//Move(h, v);

			//Turning();
		}


		private void RotationMouse()
		{
			var difference = (_camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - _transform.position)
				.normalized;
			var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			if (rotationZ >= -22.5f && rotationZ < 22.5f) _spriteRenderer.sprite = differentRotation[0];				// r
			else if (rotationZ >= 22.5f && rotationZ < 47.5f) _spriteRenderer.sprite = differentRotation[1];		// ru
			else if (rotationZ >= 47.5f && rotationZ < 112.5f) _spriteRenderer.sprite = differentRotation[2];		// u
			else if (rotationZ >= 112.5f && rotationZ < 157.5f) _spriteRenderer.sprite = differentRotation[3];		// lu
			else if (rotationZ >= 157.5f || rotationZ < -157.5f) _spriteRenderer.sprite = differentRotation[4];		// l
			else if (rotationZ >= -157.5f && rotationZ < -112.5f) _spriteRenderer.sprite = differentRotation[5];		// ld
			else if (rotationZ >= -112.5f && rotationZ < -62.5f) _spriteRenderer.sprite = differentRotation[6];		// d
			else if (rotationZ >= -62.5f && rotationZ < -22.5f) _spriteRenderer.sprite = differentRotation[7];	// dr
		}

		private void Movement()
		{
			Vector2 input = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"),
				UnityEngine.Input.GetAxisRaw("Vertical"));
			Vector2 inputDir = input.normalized;

//			if(inputDir != Vector2.zero)
//			{
//				transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
//			}

			var running = UnityEngine.Input.GetKey(KeyCode.LeftShift);
			var speed = (running ? _runSpeed : _walkSpeed) * inputDir.magnitude;

			transform.Translate(input * speed * Time.deltaTime, Space.World);

//		float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
//		animator.SetFloat("speedPercent", animationSpeedPercent);

			//if(Input.GetKey(KeyCode.W))
			//{
			//	transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
			//	if(!attacking)
			//	{
			//		bulletSpawnPoint.transform.eulerAngles = new Vector3(0, 0, 0);
			//		State = CharState.up;
			//	}
			//}
			//if(Input.GetKey(KeyCode.S))
			//{
			//	transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
			//	if(!attacking) { bulletSpawnPoint.transform.eulerAngles = new Vector3(0, 0, 180); State = CharState.down; }
			//}
			//if(Input.GetKey(KeyCode.A))
			//{
			//	sprite.flipX = true;
			//	transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
			//	if(!attacking) { bulletSpawnPoint.transform.eulerAngles = new Vector3(0, 0, 90); if(!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S)) State = CharState.sideways; }
			//	if(Input.GetKey(KeyCode.RightArrow)) { sprite.flipX = false; }
			//}
			//else sprite.flipX = false;

			//if(Input.GetKey(KeyCode.D))
			//{

			//	transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
			//	if(!attacking)
			//	{
			//		bulletSpawnPoint.transform.eulerAngles = new Vector3(0, 0, -90);
			//		if(!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S)) State = CharState.sideways;
			//	}
			//}
		}

		private void Shoot()
		{
			//waitTime += 1 * Time.deltaTime * reloadSpeed;         // Это типа время перезарядки
			//if(waitTime > 1) waitTime = 2;                       // Это типа что бы много не считала
			//if(Input.GetKey(KeyCode.UpArrow))                  // Поворачивает то чем будет стрелять(палец там руку), проходит кд и стреляет
			//{
			//	attacking = true;
			//	bulletSpawnPoint.transform.eulerAngles = new Vector3(0, 0, 0);
			//	if(waitTime > 1)
			//	{
			//		bulletSpawnPoint.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.088f, gameObject.transform.position.z);
			//		State = CharState.up;
			//		Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			//		waitTime = 0;
			//	}
			//}
			//if(Input.GetKey(KeyCode.LeftArrow))
			//{
			//	sprite.flipX = true;
			//	attacking = true;
			//	bulletSpawnPoint.transform.eulerAngles = new Vector3(0, 0, 90);
			//	if(waitTime > 1)
			//	{
			//		bulletSpawnPoint.transform.position = new Vector3(gameObject.transform.position.x - 0.618f, gameObject.transform.position.y + 0.092f, gameObject.transform.position.z);
			//		State = CharState.sideways;
			//		Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			//		waitTime = 0;
			//	}
			//}
			//else if(!Input.GetKey(KeyCode.A)) sprite.flipX = false;
			//if(Input.GetKey(KeyCode.DownArrow))
			//{
			//	attacking = true;
			//	bulletSpawnPoint.transform.eulerAngles = new Vector3(0, 0, 180);
			//	if(waitTime > 1)
			//	{
			//		bulletSpawnPoint.transform.position = new Vector3(gameObject.transform.position.x - 0.115f, gameObject.transform.position.y - 0.297f, gameObject.transform.position.z);
			//		State = CharState.down;
			//		Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			//		waitTime = 0;
			//	}
			//}
			//if(Input.GetKey(KeyCode.RightArrow))
			//{
			//	attacking = true;
			//	bulletSpawnPoint.transform.eulerAngles = new Vector3(0, 0, 270);
			//	if(waitTime > 1)
			//	{
			//		bulletSpawnPoint.transform.position = new Vector3(gameObject.transform.position.x + 0.618f, gameObject.transform.position.y + 0.092f, gameObject.transform.position.z);
			//		State = CharState.sideways;
			//		Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			//		waitTime = 0;
			//	}
			//}
			//if(Input.GetKeyUp(KeyCode.UpArrow)) attacking = false;
			//if(Input.GetKeyUp(KeyCode.LeftArrow)) attacking = false;
			//if(Input.GetKeyUp(KeyCode.DownArrow)) attacking = false;
			//if(Input.GetKeyUp(KeyCode.RightArrow)) attacking = false;
		}

		private void Turning()
		{
			// Create a ray from the mouse cursor on screen in the direction of the camera.
			Ray camRay = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

			// Create a RaycastHit variable to store information about what was hit by the ray.
			RaycastHit floorHit;

			// Perform the raycast and if it hits something on the floor layer...
			if (Physics.Raycast(camRay, out floorHit, _camRayLength, _floorMask))
			{
				// Create a vector from the player to the point on the floor the raycast from the mouse hit.
				Vector3 playerToMouse = floorHit.point - transform.position;

				// Ensure the vector is entirely along the floor plane.
				playerToMouse.y = 0f;

				// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
				Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

				// Set the player's rotation to this new rotation.
				_playerRigidbody.MoveRotation(newRotation);
			}
		}
	}
}