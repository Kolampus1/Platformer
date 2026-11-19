using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IsRunningParameter = "IsRunning";
    private const string IsJumpingParameter = "IsJumping";

    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _flipRoot;

    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale = _flipRoot.localScale;
    }

    private void Update()
    {
        _animator.SetBool(IsRunningParameter, _playerMover.IsMoving);
        _animator.SetBool(IsJumpingParameter, _playerMover.IsJumping);

        if (_playerMover.HorizontalInput < 0f)
            SetFacingLeft(true);
        else if (_playerMover.HorizontalInput > 0f)
            SetFacingLeft(false);
    }

    private void SetFacingLeft(bool isFacingLeft)
    {
        Vector3 scale = _initialScale;
        scale.x = isFacingLeft ? -_initialScale.x : _initialScale.x;
        _flipRoot.localScale = scale;
    }
}