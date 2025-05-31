using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Para que funcione hay que poner a el mago y todos sus minioins en un objeto vacio CON ESTE SCRIPT, el padre actuará como punto de respawn
//Asegurandonos así de poder desasignar a los enemigos del mago al morir pero sin perder la referencia al padre para el respawn
//al reaparecer se reasignarán los enemigos al padre y se les volverá a activar el collider y el rigidbody


public class EnemyGroupRespawn : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        // Find all enemies in the group and add them to the list
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<BaseEnemyStats>() != null)
            {
                enemies.Add(child.gameObject);
            }
        }
    }
    /* void OnDisable()
    {
        foreach (GameObject enemy in enemies)
        {
            BaseEnemyStats baseStats = enemy.GetComponent<BaseEnemyStats>();

            enemy.transform.position = transform.position;
            baseStats.currentHealth = baseStats.maxHealth;
            baseStats.dead = false;
        }
    } */

    void OnDisable()
    {
        float radius = 3f;
        int count = enemies.Count;
        for (int i = 0; i < count; i++)
        {
            BaseEnemyStats baseStats = enemies[i].GetComponent<BaseEnemyStats>();

            // Distribuimos a los enemigos equitativamente alrededor del spawn
            float angle = i * Mathf.PI * 2 / count;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            enemies[i].transform.position = transform.position + offset;

            baseStats.currentHealth = baseStats.maxHealth;
            baseStats.dead = false;

            baseStats.animator.SetBool("Death", false);

            if (baseStats.nmAgent != null)
            {
                baseStats.nmAgent.enabled = true;
            }

            // Opcional: Desactivar colisiones si es necesario
            Collider collider = baseStats.gameObject.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            baseStats.canvas.gameObject.SetActive(true);
        }
    }

    void OnEnable()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }
}
