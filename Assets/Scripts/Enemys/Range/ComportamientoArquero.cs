using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamientoArquero : MonoBehaviour
{
    public GameObject player;
    public bool inTeam = false;
    public NavMeshAgent nmAgent;

    public EnemyVisionArea visionRange;
    public EnemyVisionArea attackRange;
    public EnemyVisionArea safeSpaceRange;

    public ElementoMinion elementoMinion;
    public float distanciaAlejamiento = 2f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (inTeam)
        {
            if (attackRange.playerInRange)
            {
                if (elementoMinion.elemento > 0)
                {
                    Debug.Log("Pegar");
                }
                else
                {
                    Debug.Log("Vuelvo al area de defensa");
                }
            }
            else
            {
                Debug.Log("Defender");
            }
        }
        else
        {
            if (/*attackRange.magesInRange*/ true)
            {
                //Falta añadir la logica de mirar todos los magos que tiene cerca y añadirse al que menos minions tenga
                Debug.Log("Se asigna a un mago");
                inTeam = true;
            }
            else
            {
                if (visionRange.playerInRange)
                {
                    if (attackRange.playerInRange)
                    {
                        if (safeSpaceRange.playerInRange)
                        {
                            Debug.Log("Huir");
                            HuirDelJugador();
                        }
                        else
                        {
                            Debug.Log("Atacar");
                        }
                    }
                    else
                    {
                        Debug.Log("Perseguir");
                        PerseguirAlJugador();
                    }
                }
                else
                {
                    Debug.Log("Patrullar");
                }
            }
        }
    }

    public void HuirDelJugador()
    {
        // Calcular dirección opuesta al jugador
        Vector3 direccionAlejamiento = (transform.position - player.transform.position).normalized;

        Vector3 posicionFinal = transform.position + direccionAlejamiento * distanciaAlejamiento;
        nmAgent.SetDestination(posicionFinal);

        // Verificar si ya está fuera del rango seguro
        if (!safeSpaceRange.playerInRange)
        {
            Debug.Log("El arquero ha alcanzado un lugar seguro.");
        }
    }

    public void PerseguirAlJugador()
    {
        Vector3 direccionPerseguir = (transform.position - player.transform.position).normalized;
        Vector3 posicionFinal = transform.position - direccionPerseguir * 3f;

        // Establecer la posición del jugador como destino
        nmAgent.SetDestination(posicionFinal);
    }

}
