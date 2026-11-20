using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IsRunningParameter = "IsRunning";
    private const string IsJumpingParameter = "IsJumping";

    [SerializeField] private Animator _animator;

    public void SetRunning(bool isRunning)
    {
        _animator.SetBool(IsRunningParameter, isRunning);
    }

    public void SetJumping(bool isJumping)
    {
        _animator.SetBool(IsJumpingParameter, isJumping);
    }
}