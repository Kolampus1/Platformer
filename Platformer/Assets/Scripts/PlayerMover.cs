using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private CharacterRotator _characterRotator;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private bool _jumpRequested;
    private bool _isJumping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = _inputReader.HorizontalInput;
        _characterRotator.SetFacingDirection(horizontalInput);

        if (_inputReader.ConsumeJumpPress() && _groundDetector.IsGrounded)
        {
            _isJumping = true;
            _jumpRequested = true;
        }

        if (_isJumping && _groundDetector.IsGrounded && _jumpRequested == false && _rigidbody.linearVelocity.y <= 0f)
            _isJumping = false;

        bool isRunning = horizontalInput != 0f && _groundDetector.IsGrounded && _isJumping == false;

        _playerAnimator.SetRunning(isRunning);
        _playerAnimator.SetJumping(_isJumping);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _rigidbody.linearVelocity;

        _rigidbody.linearVelocity = new Vector2(_inputReader.HorizontalInput * _moveSpeed, velocity.y);

        if (_jumpRequested == false)
            return;

        if (_groundDetector.IsGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            _isJumping = false;
        }

        _jumpRequested = false;
    }
}