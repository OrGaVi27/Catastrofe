using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamientoArquero : MonoBehaviour
{
    [Header("Arquero basico")]
    public GameObject player;
    public bool inTeam = false;
    public bool isMoving = false;
    public bool isLookingToPlayer = false;
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
    public float damage = 10f;
    public float tiempoEntreDisparos = 1.5f;
    public bool puedeDisparar = true;
    public float tiempoUltimoDisparo = 0f;
    public GameObject flechaPrefab;

    [Header("Animaciones")]
    public Animator animator;
    private float tiempoIdle = 0f; // Temporizador
    public int idleAlt = 0;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {

        tiempoIdle += Time.deltaTime;


        if (tiempoIdle >= 5f)
        {
            idleAlt = Random.Range(0, 3);
            tiempoIdle = 0f;
        }

        if (idleAlt == 0)
        {
            animator.SetInteger("IdleAlt", 0);
        }
        else if (idleAlt == 1)
        {
            animator.SetInteger("IdleAlt", 1);
        }
        else if (idleAlt == 2)
        {
            animator.SetInteger("IdleAlt", 2);
        }


        if (nmAgent.velocity.magnitude > 0)
        {
            isMoving = true;
            animator.SetBool("IsMoving", true);
        }
        else if (nmAgent.velocity.magnitude <= 0)
        {
            isMoving = false;
            animator.SetBool("IsMoving", false);

        }


        // Hacer que el arquero siempre mire al jugador
        if (visionRange.playerInRange && isLookingToPlayer)
        {
            Vector3 direccionHaciaJugador = player.transform.position - transform.position;
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionHaciaJugador);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f); // Ajusta el factor de suavidad (5f)

        }

        //Cooldown entre ataques
        if (tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            puedeDisparar = true;
            tiempoUltimoDisparo = 0f;
        }
        else if (tiempoUltimoDisparo < tiempoEntreDisparos)
        {
            tiempoUltimoDisparo += Time.deltaTime;
        }

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
        animator.SetBool("Attacking", false);

        isLookingToPlayer = false;
        tiempoUltimoDisparo = 0f;

        // Calcular dirección opuesta al jugador
        Vector3 direccionAlejamiento = (transform.position - player.transform.position).normalized;

        Vector3 posicionFinal = transform.position + direccionAlejamiento * distanciaAlejamiento;
        nmAgent.SetDestination(posicionFinal);

        //Hace que el arquero mire en la direccion contraria al jugador
        direccionAlejamiento.y = 0;
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionAlejamiento);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f); // Ajusta el factor de suavidad (5f)


    }

    public void PerseguirAlJugador()
    {
        animator.SetBool("Attacking", false);

        isLookingToPlayer = true;

        tiempoUltimoDisparo = 0f;

        Vector3 direccionPerseguir = (transform.position - player.transform.position).normalized;
        // Vector3 posicionFinal = transform.position - direccionPerseguir * 3f;
        Vector3 posicionFinal = player.transform.position + direccionPerseguir * 10f;

        // Establecer la posición del jugador como destino
        nmAgent.SetDestination(posicionFinal);
    }

    public void DispararFlecha()
    {
        if (puedeDisparar == true && isMoving == false)
        {
            isLookingToPlayer = true;
            animator.SetBool("Attacking", true);


            puedeDisparar = false;

            // Calcular la dirección hacia el jugador
            Vector3 direccion = (player.transform.position - transform.position).normalized;
            GameObject flecha = Instantiate(flechaPrefab, transform.position, Quaternion.LookRotation(direccion));

            // Asignar el daño del arquero a la flecha
            Flecha flechaScript = flecha.GetComponent<Flecha>();
            if (flechaScript != null)
            {
                flechaScript.damage = damage;
            }

            Debug.Log("Flecha disparada hacia el jugador.");
        }
        else
        {
            Debug.Log("Flecha en cooldown");
        }

    }

}
