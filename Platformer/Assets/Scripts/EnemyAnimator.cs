using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private const string IsWalkingParameter = "IsWalking";

    [SerializeField] private Animator _animator;

    public void SetWalking(bool isWalking)
    {
        _animator.SetBool(IsWalkingParameter, isWalking);
    }
}