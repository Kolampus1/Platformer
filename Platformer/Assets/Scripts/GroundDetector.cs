using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private List<Ground> _groundContacts = new List<Ground>();

    public int GroundContactCount => _groundContacts.Count;
    public bool IsGrounded => GroundContactCount > 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground) && _groundContacts.Contains(ground) == false)
        {
            _groundContacts.Add(ground);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
            _groundContacts.Remove(ground);
    }

    private void OnDisable()
    {
        _groundContacts.Clear();
    }
}