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
    private float _inputX;

    [Header("Movement Configuration")]
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private GameObject groundCheckObject;

    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Grounded = Animator.StringToHash("IsGrounded");
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");

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
        CaptureHorizontalInput();
        ApplyWalkingDirection();
        ApplyAnimations();
    }

    private void ApplyJump()
    {
        if (IsGrounded() && _inputProvider.GetActionPressed(InputAction.Jump))
        {
            _rigidbody.SetVelocity(Axis.Y, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        return _groundCheck.Check();
    }

    private void ApplyHorizontalMovement()
    {
        _rigidbody.SetVelocity(Axis.X, _inputX * walkSpeed);
    }

    private void ApplyWalkingDirection()
    {
        if (_inputX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(_inputX), 1, 1);
        }

    }

    private void CaptureHorizontalInput()
    {
        _inputX = _inputProvider.GetAxis(Axis.X);
    }

    private void ApplyAnimations()
    {
        _animator.SetBool(IsWalking, _inputX != 0);
        _animator.SetBool(Grounded, IsGrounded());
        _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);
    }
}
