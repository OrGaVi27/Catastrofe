using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeBehavior : MonoBehaviour
{
    // Movimiento
    public NavMeshAgent minionMele;
    public Transform player;
    public Transform targetToFollow;
    public LayerMask whatIsPlayer, whatIsGround;

    // Rangos
    public float rangoDeVision;
    public float rangoDeAtaque;

    // Estados
    public bool estaEnEquipo = false;
    public bool estaEnAreaDefensa = false;
    public bool hayMagoCercaConHueco = false;

    private bool alreadyAttacked;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        minionMele = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        // Collider colliderMinion = GetComponent<Collider>();
        // GameObject magoObj = GameObject.FindWithTag("Mago");

        // if (magoObj != null)
        // {
        //     Collider colliderMago = magoObj.GetComponent<Collider>();

        //     if (colliderMinion != null && colliderMago != null)
        //     {
        //         Physics.IgnoreCollision(colliderMinion, colliderMago, true);
        //     }
        // }
    }


    void Update()
    {
        bool playerEnRangoVision = PlayerEnRangoVision();
        bool playerEnRangoPegar = PlayerEnRangoPegar();

        if (EstoyEnEquipo())
        {
            if (EstoyEnAreaDefensa())
            {
                if (playerEnRangoVision)
                {
                    if (playerEnRangoPegar)
                        Pegar();
                    else
                        Perseguir();
                }
                else
                {
                    AcercarseAlPlayer();
                }
            }
            else
            {
                PosicionarseEnAreaDefensa();
            }
        }
        else
        {
            if (TengoMagoCercaConHueco())
            {
                AsignarseAMagoConMenosAliados();
                if (playerEnRangoVision)
                {
                    if (playerEnRangoPegar)
                        Pegar();
                    else
                        Perseguir();
                }
                else
                {
                    SeguirAliado();
                }
            }
            else
            {
                if (playerEnRangoVision)
                {
                    if (playerEnRangoPegar)
                        Pegar();
                    else
                        Perseguir();
                }
                else
                {
                    SeguirAliado();
                }
            }
        }
    }

    bool EstoyEnEquipo() => estaEnEquipo;

    bool EstoyEnAreaDefensa() => estaEnAreaDefensa;

    bool TengoMagoCercaConHueco() => hayMagoCercaConHueco;

    bool PlayerEnRangoVision()
    {
        return Physics.CheckSphere(transform.position, rangoDeVision, whatIsPlayer);
    }

    bool PlayerEnRangoPegar()
    {
        return Physics.CheckSphere(transform.position, rangoDeAtaque, whatIsPlayer);
    }

    void PosicionarseEnAreaDefensa()
    {
        Debug.Log("Me posiciono en el área de defensa...");
        // minionMele.SetDestination(posiciónDefensiva);
    }

    void AsignarseAMagoConMenosAliados()
    {
        Debug.Log("Me asigno al mago con menos aliados...");
        // minionMele.SetDestination(magoConMenosAliados.position);
    }

    void AcercarseAlPlayer()
    {
        Debug.Log("Me acerco al jugador...");
        minionMele.SetDestination(player.position);
    }

    void Perseguir()
    {
        Debug.Log("Persiguiendo al jugador...");
        minionMele.SetDestination(player.position);
    }

    void SeguirAliado()
    {
        if (targetToFollow != null)
        {
            Debug.Log("Siguiendo a aliado...");
            minionMele.SetDestination(targetToFollow.position);
        }
        else
        {
            Debug.Log("No hay aliado asignado para seguir.");
        }
    }

    void Pegar()
    {
        minionMele.SetDestination(transform.position); // Se detiene
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Debug.Log("¡Atacando al jugador!");
            Invoke(nameof(ResetAttack), 1.5f);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
