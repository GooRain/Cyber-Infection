using CyberInfection.Extension;
using CyberInfection.GameMechanics.Input;
using CyberInfection.GameMechanics.Weapon;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Entity.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : UnitController, IPunObservable
    {
        private PhotonView m_PhotonView;

        [SerializeField] private GameObject m_Legs;

        [SerializeField] private Sprite[] differentRotation;

        [SerializeField] private WeaponController m_WeaponController;
        [SerializeField] private Transform m_HandPivot;

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

        private Vector3 m_MousePos;
        private int m_CurrentFrame;
        private float m_CurrentHandAngle;

        private IInputComponent m_InputComponent;

        private void Awake()
        {
            m_Transform = transform;
            m_MainCamera = UnityEngine.Camera.main;

            m_Animator = m_Legs.GetComponent<Animator>();
            m_PlayerRigidbody = GetComponent<Rigidbody2D>();
            //m_FloorMask = LayerMask.GetMask("Floor");
            m_SpriteRenderer = GetComponent<SpriteRenderer>();

            m_PhotonView = GetComponent<PhotonView>();

            if (m_PhotonView.IsMine)
            {
                m_InputComponent = new LocalInputComponent(this, m_Animator);
            }
            else
            {
                m_InputComponent = new RemoteInputComponent();
            }
        }

        private void Update()
        {
            m_InputComponent.Update();
        }

        public override void Move(Vector2 direction)
        {
            m_PlayerRigidbody.position += direction * Time.deltaTime;
        }

        public override void Rotate(Vector2 direction)
        {
            m_MousePos = m_MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            m_CurrentFrame = OctaRotationHelper.RotateFrame(m_Transform.position, m_MousePos);
            m_SpriteRenderer.sprite = differentRotation[m_CurrentFrame];
            
            m_CurrentHandAngle = OctaRotationHelper.GetAngle(m_HandPivot.position, m_MousePos);
            
            RotateHand();
        }

        private void RotateHand()
        {
            var currentHandRotation = m_HandPivot.rotation.eulerAngles;
            currentHandRotation.z = m_CurrentHandAngle;
            m_HandPivot.rotation = Quaternion.Euler(currentHandRotation);

            if (m_CurrentHandAngle < 270f && m_CurrentHandAngle > 90f)
            {
                m_HandPivot.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                m_HandPivot.localScale = Vector3.one;
            }
        }

        public override void Shoot()
        {
            m_WeaponController.Shoot();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(m_Transform.position);
                stream.SendNext(m_CurrentFrame);
                stream.SendNext(m_CurrentHandAngle);
            }
            else
            {
                m_Transform.position = (Vector3) stream.ReceiveNext();
                m_CurrentFrame = (int) stream.ReceiveNext();
                m_CurrentHandAngle = (float) stream.ReceiveNext();

                m_SpriteRenderer.sprite = differentRotation[m_CurrentFrame];
                RotateHand();
            }
        }
    }
}