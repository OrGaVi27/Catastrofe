using UnityEngine;

public class BasePlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public int maxMana = 5;
    public int currentMana;
    public float damage = 0f;
    public float DamageMultiplier = 1f;
    public bool isInvencible = false;

    public void TakeDamage(float damageAmount)
    {
        if (!isInvencible)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0f)
            {
                Die();
            }
        }

    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        Debug.Log("Player has died.");
    }
}
