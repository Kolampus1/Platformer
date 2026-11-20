using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private CharacterRotator _characterRotator;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _arrivalDistance;
    [SerializeField] private float _pauseDuration;

    private Rigidbody2D _rigidbody;
    private Transform _target;
    private bool _isWaiting;

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
            _enemyAnimator.SetWalking(false);
            StartCoroutine(WaitAtPoint());
            return;
        }

        _characterRotator.SetFacingDirection(distanceX);
        _enemyAnimator.SetWalking(_speed > 0f);

        Vector2 velocity = _rigidbody.linearVelocity;
        float horizontalSpeed = distanceX > 0 ? _speed : -_speed;

        _rigidbody.linearVelocity = new Vector2(horizontalSpeed, velocity.y);
    }

    private IEnumerator WaitAtPoint()
    {
        var wait = new WaitForSeconds(_pauseDuration);
        yield return wait;

        _target = _target == _secondPoint ? _firstPoint : _secondPoint;
        UpdateDirection();

        _isWaiting = false;
        _enemyAnimator.SetWalking(_speed > 0f);
    }

    private void StopHorizontalMovement()
    {
        Vector2 velocity = _rigidbody.linearVelocity;
        _rigidbody.linearVelocity = new Vector2(0f, velocity.y);
    }

    private void UpdateDirection()
    {
        float direction = _target.position.x - _rigidbody.position.x;
        _characterRotator.SetFacingDirection(direction);
    }
}