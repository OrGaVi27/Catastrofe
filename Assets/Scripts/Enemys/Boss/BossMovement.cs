using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    [Header("Start")]
    public Transform spawnPoint;
    public Transform player;
    public NavMeshAgent nmAgent;
    [Space]

    [Header("Movement")]
    public float movementSpeed;
    
    public void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        
        nmAgent.speed = movementSpeed;

    }

    public void Direccion()
    {
        //Calcula la dirección hacia el jugador
        Vector3 direccion = (player.position - transform.position).normalized;
        direccion.y = 0;

        //Calcula la rotación deseada
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);

        //Rota usando la velocidad angular (angularSpeed) del NavMeshAgent
        float velocidadEnAngulo = 100;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionDeseada, velocidadEnAngulo * Time.deltaTime);
    }

    public void PerseguirAlJugador()
    {
        Vector3 direccionPerseguir = (transform.position - player.transform.position).normalized;
        Vector3 posicionFinal = transform.position - direccionPerseguir * movementSpeed;

        nmAgent.SetDestination(posicionFinal);
    }

    public void StartBattle()
    {
        Debug.Log("Start Battle");

        transform.position = spawnPoint.transform.position;
    }

    public void OnEnable()
    {
        StartBattle();
    }
}
