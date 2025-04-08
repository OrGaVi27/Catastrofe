using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamientoArquero : MonoBehaviour
{
    [Header("Arquero basico")]
    public GameObject player;
    public bool inTeam = false;
    public NavMeshAgent nmAgent;

    [Header("Rangos")]
    public EnemyVisionArea visionRange;
    public EnemyVisionArea attackRange;
    public EnemyVisionArea safeSpaceRange;

    [Header("Mecanca Elemento")]
    public ElementoMinion elementoMinion;

    [Header("Distancia de alejamiento")]
    public float distanciaAlejamiento = 2f;

    [Header("Ataque a distancia")]
    public float tiempoEntreDisparos = 1.5f; // Tiempo entre disparos
    public bool puedeDisparar = true; // Controla si el arquero puede disparar

    public GameObject flechaPrefab; // Prefab de la flecha
    public float velocidadFlecha = 10f; // Velocidad de la flecha

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
                    Debug.Log("Atacar");
                    DispararFlecha();
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
            if (attackRange.magesInRange)
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
                            // puedeDisparar = false;
                            HuirDelJugador();
                        }
                        else
                        {
                            Debug.Log("Atacar");
                            DispararFlecha();
                        }
                    }
                    else
                    {
                        Debug.Log("Perseguir");
                        // puedeDisparar = false;
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
        // Vector3 posicionFinal = transform.position - direccionPerseguir * 3f;
        Vector3 posicionFinal = player.transform.position + direccionPerseguir * 8f;

        // Establecer la posición del jugador como destino
        nmAgent.SetDestination(posicionFinal);
    }

    public void DispararFlecha()
    {
        if (puedeDisparar)
        {
            StartCoroutine(DispararFlechaCoroutine());
        }
    }
    private IEnumerator DispararFlechaCoroutine()
    {
        puedeDisparar = false;

        // Calcular la dirección hacia el jugador
        Vector3 direccion = (player.transform.position - transform.position).normalized;

        // Instanciar la flecha en el punto de disparo con la rotación adecuada
        GameObject flecha = Instantiate(flechaPrefab, transform.position, Quaternion.LookRotation(direccion));

        // Asignar velocidad a la flecha
        Rigidbody rb = flecha.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direccion * velocidadFlecha;
        }

        Debug.Log("Flecha disparada hacia el jugador.");

        // Esperar antes de permitir otro disparo
        yield return new WaitForSeconds(tiempoEntreDisparos);
        puedeDisparar = true;
    }

}
