
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    private int coins;
    [SerializeField] private TMP_Text coinsDisplay;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

    }

    private void Start()
    {
        coinsDisplay.text = "123";
    }

    private void OnGUI()
    {
        coinsDisplay.text = coins.ToString();
    }
    
    public void ChangeCoins(int amount)
    {
        coins += amount;
    }
}
