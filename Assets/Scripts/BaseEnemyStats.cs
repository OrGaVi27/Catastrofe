using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyStats : MonoBehaviour
{
    public float maxHealth = 100f;
    protected float currentHealth;
    public float damage = 0f;
    // public float movementSpeed = 1f;

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0f)
        {
            Die();
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

    public void Die(){ }
}
