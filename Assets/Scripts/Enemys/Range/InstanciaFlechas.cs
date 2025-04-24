using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InstanciaFlechas : MonoBehaviour
{
    [Header("Arquero basico")]
    public GameObject player;
    public GameObject flechaPrefab;
    public float damage = 10f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void InstanciarFlecha()
    {
        // Calcular la dirección hacia el jugador
        // Vector3 direccion = (player.transform.position - transform.position).normalized;

        // Vector3 direccion = (player.transform.position - transform.position).normalized;
        // Vector3 posicionFlecha = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        // GameObject flecha = Instantiate(flechaPrefab, posicionFlecha, Quaternion.LookRotation(direccion));

        // Calcular la dirección hacia el jugador
        Vector3 direccion = (player.transform.position - transform.position).normalized;
        Vector3 posicionFlecha = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Debug.Log("Direccion flecha: " + direccion);
        GameObject flecha = Instantiate(flechaPrefab, posicionFlecha, Quaternion.LookRotation(direccion));

        // Asignar el daño del arquero a la flecha
        Flecha flechaScript = flecha.GetComponent<Flecha>();
        if (flechaScript != null)
        {
            flechaScript.damage = damage;
        }

        Debug.Log("Flecha disparada hacia el jugador.");
    }

}
