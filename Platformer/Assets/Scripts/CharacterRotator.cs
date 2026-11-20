using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    private const float HalfTurn = 180f;

    private bool _isFacingRight = true;

    public void SetFacingDirection(float horizontalDirection)
    {
        if (horizontalDirection < 0f && _isFacingRight)
        {
            transform.Rotate(0f, HalfTurn, 0f, Space.World);
            _isFacingRight = false;
        }
        else if (horizontalDirection > 0f && _isFacingRight == false)
        {
            transform.Rotate(0f, HalfTurn, 0f, Space.World);
            _isFacingRight = true;
        }
    }
}