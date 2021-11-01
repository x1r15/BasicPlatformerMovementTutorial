using Enums;
using Extensions;
using Interfaces;
using UnityEngine;

namespace Behaviours.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(IInputProvider))]
    public class PlatformMovement : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private IInputProvider _inputProvider;
        private ICheck _groundCheck;

        [Header("Movement Configuration")]
        [SerializeField]
        private float walkSpeed;

        [SerializeField]
        private float jumpForce;

        [SerializeField]
        private GameObject groundCheckObject;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _inputProvider = GetComponent<IInputProvider>();
            _groundCheck = groundCheckObject.GetComponent<ICheck>();
        }

        private void FixedUpdate()
        {
            ApplyHorizontalMovement();
            ApplyJump();
        }

        private void Update()
        {
            ApplyAnimation();
        }

        private void ApplyHorizontalMovement()
        {
            var inputX = _inputProvider.GetAxisInput(Axis.X);
            _rigidbody.SetVelocity(Axis.X, inputX * walkSpeed);
        }

        private void ApplyJump()
        {
            if (IsGrounded() && _inputProvider.GetActionPressed(InputAction.Jump))
            {
                _rigidbody.SetVelocity(Axis.Y, jumpForce);
                _animator.SetTrigger("Jump");
            }
        }

        private bool IsGrounded()
        {
            return _groundCheck.Check();
        }

        private void ApplyAnimation()
        {
            var inputX = _inputProvider.GetAxisInput(Axis.X);
            if (inputX != 0)
            {
                var scale = transform.localScale;
                transform.localScale = new Vector3(Mathf.Sign(inputX), scale.y, scale.z);
            }

            if (!IsGrounded())
            {
                _animator.SetBool("IsFalling", _rigidbody.velocity.y < 0);
            }

            _animator.SetBool("IsWalking", inputX != 0);
            _animator.SetBool("IsGrounded", IsGrounded());
        }
    }
}
