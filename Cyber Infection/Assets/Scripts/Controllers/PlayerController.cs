using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float walkSpeed;
	[SerializeField]
	private float runSpeed;
	[SerializeField]
	private float reloadSpeed;
	//[SerializeField]
	//private GameObject bulletSpawnPoint;
	//[SerializeField]
	//private GameObject bullet;            // пуля которая будет стрелять

	private SpriteRenderer sprite;
	private Animator animator;
	private CharState State
	{
		get { return (CharState)animator.GetInteger("State"); }
		set { animator.SetInteger("State", (int)value); }
	}


	private float waitTime;
	private bool attacking;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	private void Update()
	{
		if(!attacking) State = CharState.idle;
		Movement();
		Shoot();
	}

	private void Movement()
	{

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;

		if(inputDir != Vector2.zero)
		{
			transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
		}

		bool running = Input.GetKey(KeyCode.LeftShift);
		float speed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;

		transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

		float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
		animator.SetFloat("speedPercent", animationSpeedPercent);

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
}