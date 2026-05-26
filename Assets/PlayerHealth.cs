using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10;
    private float health = 5;
    private bool canReceiveDamage = true;
    public float invincililitqTimer = 2;
    private object animator;

    public delegate void HealthChangeHandler(float newHealth, float amountChanged);
    public event HealthChangeHandler OnHealthChanged;
    public delegate void HealthInitHandler(float newHealth);
    public event HealthInitHandler OnHealthInitialised;
    private const string FlashRedAnim = "FlashRed";

    void Start()
    {
        health = maxHealth;
        OnHealthInitialised?.Invoke(health);
    }

    void Update()
    {

    }

    public object GetAnimator()
    {
        return animator;
    }

    public void AddDamage(float damage, Collider2D collider2D)
    {
        if (canReceiveDamage)
        {
            health -= damage;
            OnHealthChanged?.Invoke(health, -damage);

            // Trigger effects
            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.Shake(0.15f, 0.2f);
            }

            // Start damage cooldown and hitstop together
            StartCoroutine(DamageCooldownRoutine());
        }

        if (health <= 0)
        {
            SceneManager.LoadScene("GAME FAIL");
        }
    }

    public void AddHealth(float healthToAdd)
    {
        health += healthToAdd;
        OnHealthChanged?.Invoke(health, healthToAdd);
        Debug.Log(health);
    }

    // Handles hitstop effect and resets invincibility after the timer ends
    private IEnumerator DamageCooldownRoutine()
    {
        canReceiveDamage = false; // Disable damage taken

        // Hitstop effect (Freeze game)
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;

        // Wait for the remaining invincibility time before allowing damage again
        // We use Realtime because timeScale changes can mess up normal time
        yield return new WaitForSecondsRealtime(invincililitqTimer - 0.1f);

        canReceiveDamage = true; // Enable damage taken again
    }
}
