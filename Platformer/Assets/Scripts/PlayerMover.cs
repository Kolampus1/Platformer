using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _jumpRequested;

    public float HorizontalInput => _horizontalInput;
    public bool IsMoving => _horizontalInput != 0f;
    public bool IsGrounded => _isGrounded;
    public bool IsJumping => _isJumping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw(HorizontalAxis);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            _isJumping = true;
            _jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _rigidbody.linearVelocity;

        _rigidbody.linearVelocity = new Vector2(_horizontalInput * _moveSpeed, velocity.y);

        if (_jumpRequested)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _jumpRequested = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
        {
            _isGrounded = true;
            _isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
            _isGrounded = false;
    }
}