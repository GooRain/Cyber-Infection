using CyberInfection.Extension;
using CyberInfection.GameMechanics.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace CyberInfection.GameMechanics.Unit.Player
{
    public class PlayerController : UnitController
    {
        [SerializeField] private float m_WalkSpeed;

        [SerializeField] private float m_RunSpeed;

        [SerializeField] private GameObject m_Legs;

        [SerializeField] private Sprite[] differentRotation;

        [SerializeField] private WeaponController m_WeaponController;

        private SpriteRenderer m_SpriteRenderer;

        private Animator m_Animator;
        //private CharState State
        //{
        //	get { return (CharState)animator.GetInteger("State"); }
        //	set { animator.SetInteger("State", (int)value); }
        //}

        private float m_WaitTime;
        private bool m_IsAttacking;

        private Vector3 _movement; // The vector to store the direction of the player's movement.
        private Rigidbody2D m_PlayerRigidbody; // Reference to the player's rigidbody.
        //private int m_FloorMask; // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        private UnityEngine.Camera m_MainCamera;
        private Transform m_Transform;

        private void Awake()
        {
            m_Transform = transform;
            m_MainCamera = UnityEngine.Camera.main;
            
            m_Animator = m_Legs.GetComponent<Animator>();
            m_PlayerRigidbody = GetComponent<Rigidbody2D>();
            //m_FloorMask = LayerMask.GetMask("Floor");
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleRotation()
        {
            m_SpriteRenderer.sprite =
                differentRotation[
                    EightWayRotation.RotateFrame(m_Transform.position,
                        m_MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition))];
        }

        private void HandleInput()
        {
            HandleMovement();
            HandleRotation();
            HandleAction();
        }

        private void HandleAction()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

        private void HandleMovement()
        {
            var input = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"),
                UnityEngine.Input.GetAxisRaw("Vertical"));

            m_Animator.SetFloat("Horizontal", input.x);
            m_Animator.SetFloat("Vertical", input.y);
            m_Animator.SetFloat("Magnitude", input.magnitude);

            Move(input.normalized);
        }

        private void Shoot()
        {
            m_WeaponController.Shoot();
        }

        public override void Rotate(Vector2 direction)
        {
            HandleRotation();
        }

        public override void Move(Vector2 direction)
        {
            var running = UnityEngine.Input.GetKey(KeyCode.LeftShift);
            var speed = running ? m_RunSpeed : m_WalkSpeed;
            m_PlayerRigidbody.MovePosition(m_PlayerRigidbody.position + direction * speed * Time.deltaTime);
        }
    }
}