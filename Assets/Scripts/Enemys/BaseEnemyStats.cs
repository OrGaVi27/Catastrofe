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

    public void Die()
    {
        animator.SetBool("Death", true);

       /*  if (nmAgent != null)
        {
            nmAgent.enabled = false;
        } */

        // Opcional: Desactivar colisiones si es necesario
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Invoke("Desaparecer", 15f); // Llama a Desaparecer después de 15 segundos
    }

    public void Desaparecer()
    {
        Vector3 nuevaPosicion = transform.position;
        nuevaPosicion.y = -10f;

        Vector3 posicionInicial = transform.position;
        Vector3 posicionFinal = new Vector3(posicionInicial.x, -10f, posicionInicial.z);
        float duracion = 5f; // Duración del descenso en segundos
        float tiempo = 0f;
        transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempo / duracion);
        tiempo += Time.deltaTime;

        Invoke("DestroyObject", 10f);

    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
