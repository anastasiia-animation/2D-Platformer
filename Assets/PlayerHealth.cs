using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10;
    private float health = 5;
    private bool canReceiveDamage = true;
    public float invincililitqTimer = 2;

    public delegate void HealthChangeHandler(float newHealth, float amountChanged);
    public event HealthChangeHandler OnHealthChanged;
    public delegate void HealthInitHandler(float newHealth);
    public event HealthInitHandler OnHealthInitialised;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        OnHealthInitialised?.Invoke(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddDamage(float damage)
    {
        if (canReceiveDamage)
        {
            health -= damage;
            OnHealthChanged?.Invoke(health, -damage);
            canReceiveDamage = false;
            StartCoroutine(InvincibilityTimer(invincililitqTimer, ResetInvincibility));
        }

        Debug.Log(health);
    }

    IEnumerator InvincibilityTimer(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback.Invoke();
    }

    private void ResetInvincibility()
    {
        canReceiveDamage = true;
    }

    public void AddHealth(float healthToAdd)
    {
        health += healthToAdd;
        OnHealthChanged?.Invoke(health, healthToAdd);
        Debug.Log(health);
    }

}
    