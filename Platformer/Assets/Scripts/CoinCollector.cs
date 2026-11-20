using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Coin coin = other.GetComponent<Coin>();

        if (coin != null)
            coin.Collect();
    }
}