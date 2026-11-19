using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Collected;

    public Transform SpawnPoint { get; private set; }

    private bool _hasBeenCollected;

    public void Initialize(Transform spawnPoint)
    {
        SpawnPoint = spawnPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasBeenCollected)
            return;

        PlayerMover player = other.GetComponent<PlayerMover>();

        if (player == null)
            return;

        _hasBeenCollected = true;
        Collected?.Invoke(this);
    }
}