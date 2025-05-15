using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BaseEnemyStats : MonoBehaviour
{
    [Header("Stats")]

    public float maxHealth = 100f;
    public float currentHealth;
    public float damage = 0f;
    public float movementSpeed = 1f;

    public bool dead = false;

    [Header("Animaciones")]
    public Animator animator;

    [Header("NavMesh")]
    public NavMeshAgent nmAgent;

    [Header("Canvas")]
    public Canvas canvas;


    public void TakeDamage(float damageAmount, int element)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0f)
        {
            dead = true;
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

    public void Die()
    {
        Debug.Log("Muerto");

        animator.SetBool("Death", true);

        dead = true;

        if (nmAgent != null)
        {
            nmAgent.enabled = false;
        }

        // Opcional: Desactivar colisiones si es necesario
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        canvas.gameObject.SetActive(false); // Desactiva el canvas del enemigo

        Invoke("DestroyObject", 10f); // Llama a Desaparecer después de 15 segundos
    }

    public void DestroyObject()
    {
        Debug.Log("Desaparecer");
        Destroy(gameObject);
    }
}
