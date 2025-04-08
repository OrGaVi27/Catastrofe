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
    
    void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        
        nmAgent.speed = movementSpeed;
    }

    void Update()
    {
        PerseguirAlJugador();
    }

    public void PerseguirAlJugador()
    {
        Vector3 direccionPerseguir = (transform.position - player.transform.position).normalized;
        Vector3 posicionFinal = transform.position - direccionPerseguir * movementSpeed;

        // Establecer la posición del jugador como destino
        nmAgent.SetDestination(posicionFinal);
    }

    public void StartBattle()
    {
        Debug.Log("Start Battle");

        transform.position = spawnPoint.transform.position;
    }

    void OnEnable()
    {
        StartBattle();
    }
}
