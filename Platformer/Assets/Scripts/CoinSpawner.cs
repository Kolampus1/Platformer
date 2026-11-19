using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    private const float MinDelay = 2;
    private const float MaxDelay = 5;

    [SerializeField] private Coin _coin;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            if (spawnPoint != null)
                SpawnCoinAt(spawnPoint);
        }
    }

    private void SpawnCoinAt(Transform spawnPoint)
    {
        Coin coin = Instantiate(_coin, spawnPoint.position, spawnPoint.rotation);

        coin.Initialize(spawnPoint);
        coin.Collected += HandleCoinCollected;
    }

    private void HandleCoinCollected(Coin coin)
    {
        coin.Collected -= HandleCoinCollected;

        Transform spawnPoint = coin.SpawnPoint;
        Destroy(coin.gameObject);

        StartCoroutine(RespawnAfterDelay(spawnPoint));
    }

    private IEnumerator RespawnAfterDelay(Transform spawnPoint)
    {
        float delay = Random.Range(MinDelay, MaxDelay);

        var wait = new WaitForSeconds(delay);
        yield return wait;

        if (spawnPoint != null)
            SpawnCoinAt(spawnPoint);
    }
}