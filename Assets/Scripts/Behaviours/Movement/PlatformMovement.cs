using Enums;
using Extensions;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IInputProvider))]
public class PlatformMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private IInputProvider _inputProvider;
    private ICheck _groundCheck;
    private Animator _animator;

    [Header("Movement Configuration")]
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private GameObject groundCheckObject;

    private static readonly int IsFalling = Animator.StringToHash("IsFalling");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Grounded = Animator.StringToHash("IsGrounded");
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputProvider = GetComponent<IInputProvider>();
        _groundCheck = groundCheckObject.GetComponent<ICheck>();
        _animator = GetComponent<Animator>();
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

    private void ApplyAnimation()
    {
        var inputX = _inputProvider.GetAxisInput(Axis.X);
        if (inputX != 0)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Sign(inputX), scale.y, scale.z);
        }

        _animator.SetBool(IsFalling, !IsGrounded() &&_rigidbody.velocity.y < 0);
        _animator.SetBool(IsWalking, inputX != 0);
        _animator.SetBool(Grounded, IsGrounded());
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
            _animator.SetTrigger(Jump);
        }
    }

    private bool IsGrounded()
    {
        return _groundCheck.Check();
    }
}
