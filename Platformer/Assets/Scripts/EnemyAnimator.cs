using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private const string IsWalkingParameter = "IsWalking";

    [SerializeField] private EnemyMover _enemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _flipRoot;

    private Vector3 _rightScale;
    private Vector3 _leftScale;

    private void Awake()
    {
        _rightScale = _flipRoot.localScale;
        _leftScale = _rightScale;
        _leftScale.x *= -1f;
    }

    private void Update()
    {
        _animator.SetBool(IsWalkingParameter, _enemy.IsMoving);

        _flipRoot.localScale = _enemy.IsMovingRight ? _rightScale : _leftScale;
    }
}