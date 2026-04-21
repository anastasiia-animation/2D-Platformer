using UnityEngine;

public class Coin : MonoBehaviour
{
    internal int coinCount;
    [SerializeField] private int value;

    private CoinManager CoinManager;

    private void Start()
    {
        CoinManager = CoinManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinManager.ChangeCoins(value);
            Destroy(gameObject);
        }
    }
}
