using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Collected;

    public Transform SpawnPoint { get; private set; }

    private bool _isCollected;

    public void Initialize(Transform spawnPoint)
    {
        SpawnPoint = spawnPoint;
    }

    public void Collect()
    {
        _isCollected = true;
        Collected?.Invoke(this);
    }
}