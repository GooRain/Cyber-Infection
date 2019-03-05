using CyberInfection.Extension.Utility;
using CyberInfection.GameMechanics.Unit;
using UnityEngine;

namespace CyberInfection.GameMechanics.Input
{
    public class LocalInputComponent : IInputComponent
    {
        private UnitController m_UnitController;
        private Animator m_Animator;

        public LocalInputComponent(UnitController unitController, Animator animator)
        {
            m_UnitController = unitController;
            m_Animator = animator;
        }

        public void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleAction();
        }

        private void HandleRotation()
        {
            m_UnitController.Rotate(Vector2.zero);
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

            m_Animator.SetFloat(AnimatorHash.Horizontal, input.x);
            m_Animator.SetFloat(AnimatorHash.Vertical, input.y);
            m_Animator.SetFloat(AnimatorHash.Magnitude, input.magnitude);

            Move(input.normalized);
        }

        private void Shoot()
        {
            m_UnitController.Shoot();
        }

        private void Move(Vector2 direction)
        {
            var running = UnityEngine.Input.GetKey(KeyCode.LeftShift);
            var speed = running ? m_UnitController.runSpeed : m_UnitController.walkSpeed;

            m_UnitController.Move(direction * speed);
        }
    }
}