using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _arrivalDistance;
    [SerializeField] private float _pauseDuration;

    private Rigidbody2D _rigidbody;
    private Transform _target;
    private bool _isWaiting;

    public bool IsMoving => _target != null && _isWaiting == false && _speed > 0f;
    public bool IsMovingRight { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _target = _secondPoint;
        UpdateDirection();
    }

    private void FixedUpdate()
    {
        if (_isWaiting)
        {
            StopHorizontalMovement();
            return;
        }

        float distanceX = _target.position.x - _rigidbody.position.x;

        if (distanceX * distanceX <= _arrivalDistance * _arrivalDistance)
        {
            _isWaiting = true;
            StopHorizontalMovement();
            StartCoroutine(WaitAtPoint());
            return;
        }

        IsMovingRight = distanceX > 0f;

        Vector2 velocity = _rigidbody.linearVelocity;
        float horizontalSpeed = IsMovingRight ? _speed : -_speed;

        _rigidbody.linearVelocity = new Vector2(horizontalSpeed, velocity.y);
    }

    private IEnumerator WaitAtPoint()
    {
        yield return new WaitForSeconds(_pauseDuration);

        _target = _target == _secondPoint ? _firstPoint : _secondPoint;

        UpdateDirection();
        _isWaiting = false;
    }

    private void StopHorizontalMovement()
    {
        Vector2 velocity = _rigidbody.linearVelocity;
        _rigidbody.linearVelocity = new Vector2(0f, velocity.y);
    }

    private void UpdateDirection()
    {
        IsMovingRight = _target.position.x > _rigidbody.position.x;
    }
}