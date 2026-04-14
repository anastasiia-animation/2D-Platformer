using TMPro;
using UnityEngine;

public class UIHealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public PlayerHealth PlayerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealth.OnHealthChanged += OnHealthChanged;
        PlayerHealth.OnHealthInitialised += OnHealthInit;
    }

    private void OnHealthInit(float newHealth)
    {
        healthText.text = newHealth.ToString();
    }
    public void OnHealthChanged(float newHealth, float amountChanged)
    {
       //Debug.Log("On Health Changed Event");
       healthText.text = newHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
