using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeBehavior : MonoBehaviour
{
    public NavMeshAgent minionMele;
    public Transform player;
    public Transform targetToFollow;
    public LayerMask whatIsPlayer, whatIsGround;

    public GameObject rangoDeVision;
    public float rangoDeAtaque;

    public bool estaEnEquipo = false;
    public bool estoyEnUnEquipo = false;

    private bool alreadyAttacked;
    private bool IsMoving = false;
    private bool isLookingToPlayer = false;

    private Animator animator;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        minionMele = GetComponent<NavMeshAgent>();

        GetComponentInChildren<Animator>();
        animator = GetComponentInChildren<Animator>();
        minionMele.updateRotation = false;

    }

    void Update()
    {
        // Animaciones Transiciones Caminar
        if (minionMele.velocity.magnitude > 0.5)
        {
            IsMoving = true;
            animator.SetBool("IsMoving", true);
        }
        else if (minionMele.velocity.magnitude <= 0.5)
        {
            IsMoving = false;
            animator.SetBool("IsMoving", false);
        }

        // Hacer que el enemigo mire al jugador
        if (PlayerEnRangoVision() && isLookingToPlayer)
        {
            Vector3 direccionHaciaJugador = (player.position - transform.position);
            direccionHaciaJugador.y = 0f;
            direccionHaciaJugador = direccionHaciaJugador.normalized;

            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionHaciaJugador);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 10f);
        }

        // Comportamiento cuando el enemigo está en un equipo
        if (estoyEnUnEquipo)
        {
            if (EstoyEnAreaDeDefensa())
            {
                if (PlayerEnRangoVision())
                {
                    if (PlayerEnRangoDePegar())
                        Pegar(); // Atacar cuerpo a cuerpo
                    else
                        Perseguir(); // Seguir al jugador
                }
                else
                {
                    SeguirAliado(); // Seguir a un aliado si no está en rango de visión
                }
            }
            else
            {
                PosicionarseEnAreaDefensa(); // Posicionarse en el área de defensa
            }
        }
        else
        {
            // Comportamiento cuando el enemigo no está en un equipo
            if (HayMagoDisponibleCercano())
            {
                AsignarseAMagoConMenosAliados(); // Buscar un mago cercano para unirse
            }
            else
            {
                if (PlayerEnRangoVision())
                {
                    if (PlayerEnRangoDePegar())
                        Pegar(); // Atacar cuerpo a cuerpo
                    else
                        Perseguir(); // Seguir al jugador
                }
                else
                {
                    SeguirAliado(); // Seguir a un aliado
                }
            }
        }
    }

    bool PlayerEnRangoVision()
    {
        SphereCollider sphCol = rangoDeVision.GetComponent<SphereCollider>();
        float radio = sphCol.radius * rangoDeVision.transform.lossyScale.x;
        return Physics.CheckSphere(transform.position, radio, whatIsPlayer);
    }

    bool PlayerEnRangoDePegar()
    {
        return Physics.CheckSphere(transform.position, rangoDeAtaque, whatIsPlayer);
    }

    bool EstoyEnAreaDeDefensa()
    {
        if (targetToFollow == null) return false;
        float distancia = Vector3.Distance(transform.position, targetToFollow.position);
        float areaDefensa = 5f;
        return distancia < areaDefensa;
    }

    bool HayMagoDisponibleCercano()
    {
        GameObject[] magos = GameObject.FindGameObjectsWithTag("Mago");
        foreach (GameObject magoObj in magos)
        {
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

    void AsignarseAMagoConMenosAliados()
    {
        int menorCantidad = int.MaxValue;
        MageBehaviour mejorMago = null;

        GameObject[] magos = GameObject.FindGameObjectsWithTag("Mago");

        foreach (GameObject magoObj in magos)
        {
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

    void Perseguir()
    {
        isLookingToPlayer = true;
        minionMele.SetDestination(player.position);
        Debug.Log("Persiguiendo al jugador");
    }

    void Pegar()
    {
        minionMele.SetDestination(transform.position);
        isLookingToPlayer = true;
        animator.SetBool("CanAttack", true);
        animator.SetBool("Attacking", true);


        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Debug.Log("¡Atacando al jugador!");
            Invoke(nameof(ResetAttack), 1.5f);
        }
    }

    void SeguirAliado()
    {
        isLookingToPlayer = false;
        if (targetToFollow != null)
        {
            Debug.Log("Acompañando a un aliado");
            float distanciaMinima = 2.0f; 
            Vector3 direccion = targetToFollow.position - transform.position;
            direccion.y = 0;

            if (direccion.magnitude > distanciaMinima)
            {
                Vector3 destino = targetToFollow.position - direccion.normalized * distanciaMinima;
                minionMele.SetDestination(destino);
            }
            else
            {
                minionMele.SetDestination(transform.position); 
            }
        }
    }

    void PosicionarseEnAreaDefensa()
    {
        isLookingToPlayer = false;
        if (targetToFollow != null)
        {
            Debug.Log("Posicionándose en el área de defensa");
            float distanciaMinima = 2.0f;
            Vector3 direccion = targetToFollow.position - transform.position;
            direccion.y = 0;

            if (direccion.magnitude > distanciaMinima)
            {
                Vector3 destino = targetToFollow.position - direccion.normalized * distanciaMinima;
                minionMele.SetDestination(destino);
            }
            else
            {
                minionMele.SetDestination(transform.position); 
            }
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
