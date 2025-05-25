using System.Collections;
using System.Collections.Generic;
// using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ComportamientoArquero : BaseEnemyStats
{
    [Header("Arquero basico")]
    public GameObject player;
    public bool isMoving = false;
    public bool isLookingToPlayer = false;


    [Header("Rangos")]
    public EnemyVisionArea visionRange;
    public EnemyVisionArea attackRange;
    public EnemyVisionArea safeSpaceRange;


    [Header("Mecanca Elemento")]
    public ElementoMinion elementoMinion;


    [Header("Distancia de alejamiento")]
    public float distanciaAlejamiento = 2f;


    [Header("Ataque a distancia")]
    public float tiempoEntreDisparos = 1.5f;
    public bool puedeDisparar = true;
    public float tiempoUltimoDisparo = 0f;
    public GameObject flechaPrefab;

    [Header("Animaciones")]
    private float tiempoIdle = 0f; // Temporizador
    public int idleAlt = 0;


    [Header("Comportamiento Mago")]
    public Transform targetToFollow;
    public bool estoyEnUnEquipo = false;
    public bool estaEnEquipo = false;
    public float distanciaMago = 10f;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player");
        nmAgent.speed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            if (estoyEnUnEquipo)
            {
                GameObject[] team = targetToFollow.gameObject.GetComponent<MageBehaviour>().teamMembers;
                for (int i = 0; i < team.Length; i++)
                {
                    if (team[i] == gameObject)
                    {
                        team[i] = null;
                        break;
                    }
                }
            }
            return;
        }

        //Si el mago al que sigue el arquero muere, se desactiva el equipo
        if (estoyEnUnEquipo)
        {
            if (targetToFollow.GetComponent<BaseEnemyStats>().dead)
            {
                elementoMinion.elemento = 0;
                estoyEnUnEquipo = false;
            }
        }
        

        //Se usa para calcular el daño de los ataques
        element =  elementoMinion.elemento;


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


        if (nmAgent.velocity.magnitude > 0.5)
        {
            isMoving = true;
            animator.SetBool("IsMoving", true);
        }
        else if (nmAgent.velocity.magnitude <= 0.5)
        {
            isMoving = false;
            animator.SetBool("IsMoving", false);

        }


        // Hacer que el arquero siempre mire al jugador
        if (visionRange.playerInRange && isLookingToPlayer && dead == false)
        {
            Vector3 direccionHaciaJugador = (player.transform.position - transform.position).normalized;
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionHaciaJugador);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f); // Ajusta el factor de suavidad (5f)
        }


        //Cooldown entre ataques
        if (tiempoUltimoDisparo <= tiempoEntreDisparos)
        {
            tiempoUltimoDisparo += Time.deltaTime;
            animator.SetBool("CanAttack", false);

        }
        else if (tiempoUltimoDisparo > tiempoEntreDisparos)
        {
            puedeDisparar = true;

        }


        if (estoyEnUnEquipo && dead == false)
        {
            if (safeSpaceRange.playerInRange)
            {
                // Debug.Log("Huir");
                HuirDelJugador();
            }
            else
            {
                if (attackRange.playerInRange)
                {
                    if (elementoMinion.elemento > 0)
                    {
                        // Debug.Log("Atacar");
                        DispararFlecha();
                    }
                    else
                    {
                        if (targetToFollow != null)
                        {
                            // Debug.Log("Vuelvo al area de defensa");
                            SeguirAliado();
                        }

                    }
                }
                else
                {
                    if (targetToFollow != null)
                    {
                        // Debug.Log("Defender");
                        SeguirAliado();
                    }
                }
            }

        }
        else if (dead == false)
        {
            if (!estaEnEquipo && HayMagoDisponibleCercano(distanciaMago))
            {

                // Debug.Log("Se asigna a un mago");
                AsignarseAMagoConMenosAliados(distanciaMago);

            }
            else
            {
                if (visionRange.playerInRange)
                {
                    if (attackRange.playerInRange)
                    {
                        if (safeSpaceRange.playerInRange)
                        {
                            // Debug.Log("Huir");
                            HuirDelJugador();
                        }
                        else
                        {
                            // Debug.Log("Atacar");
                            DispararFlecha();
                        }
                    }
                    else
                    {
                        // Debug.Log("Perseguir");
                        PerseguirAlJugador();
                    }
                }
                else
                {
                    // Debug.Log("Patrullar");
                    SeguirAliado();
                }
            }
        }

    }

    public void HuirDelJugador()
    {

        animator.SetBool("Attacking", false);

        isLookingToPlayer = false;

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
            tiempoUltimoDisparo = 0;
            isLookingToPlayer = true;

            animator.SetBool("CanAttack", true);
            animator.SetBool("Attacking", true);

            //La flecha se instanciará en el script de la flecha y se llama en la animacion

            puedeDisparar = false;


        }

    }


    bool HayMagoDisponibleCercano(float rango = 10f)
    {
        GameObject[] magos = GameObject.FindGameObjectsWithTag("Mago");

        foreach (GameObject magoObj in magos)
        {
            float distancia = Vector3.Distance(transform.position, magoObj.transform.position);
            if (distancia > rango) continue;

            MageBehaviour mago = magoObj.GetComponent<MageBehaviour>();
            if (mago == null) continue;

            foreach (GameObject miembro in mago.teamMembers)
            {
                if (miembro == null)
                    return true;
            }
        }

        return false;
    }

    void AsignarseAMagoConMenosAliados(float rango = 10f)
    {
        int menorCantidad = int.MaxValue;
        MageBehaviour mejorMago = null;

        GameObject[] magos = GameObject.FindGameObjectsWithTag("Mago");

        foreach (GameObject magoObj in magos)
        {
            float distancia = Vector3.Distance(transform.position, magoObj.transform.position);
            if (distancia > rango) continue;

            MageBehaviour mago = magoObj.GetComponent<MageBehaviour>();
            if (mago == null) continue;

            int cantidadActual = 0;
            foreach (GameObject miembro in mago.teamMembers)
                if (miembro != null) cantidadActual++;

            if (cantidadActual < 5 && cantidadActual < menorCantidad)
            {
                menorCantidad = cantidadActual;
                mejorMago = mago;
            }
        }

        if (mejorMago != null)
        {
            for (int i = 0; i < mejorMago.teamMembers.Length; i++)
            {
                if (mejorMago.teamMembers[i] == null)
                {
                    mejorMago.teamMembers[i] = gameObject;
                    targetToFollow = mejorMago.transform;
                    estoyEnUnEquipo = true;
                    estaEnEquipo = true;
                    break;
                }
            }
        }
    }
    void SeguirAliado()
    {
        if (targetToFollow != null)
        {
            // Debug.Log("Acompañando Aliado");

            // Calcular la dirección hacia el mago
            Vector3 direccionHaciaMago = (targetToFollow.position - transform.position).normalized;

            // Calcular la posición objetivo a 2 unidades del mago
            Vector3 posicionObjetivo = targetToFollow.position - direccionHaciaMago * 4f;

            // Establecer la posición objetivo en el NavMeshAgent
            nmAgent.SetDestination(posicionObjetivo);
        }
    }


}
