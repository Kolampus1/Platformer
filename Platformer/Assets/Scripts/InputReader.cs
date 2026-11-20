using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    private bool _jumpPressed;

    public float HorizontalInput { get; private set; }

    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw(HorizontalAxis);

        if (Input.GetKeyDown(KeyCode.Space))
            _jumpPressed = true;
    }

    public bool ConsumeJumpPress()
    {
        bool jumpPressed = _jumpPressed;
        _jumpPressed = false;

        return jumpPressed;
    }
}