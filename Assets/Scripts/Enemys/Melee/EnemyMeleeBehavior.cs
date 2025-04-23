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
    // public float rangoDeVision;
    public GameObject rangoDeVision;
    public float rangoDeAtaque;

    // Estados
    public bool estaEnEquipo = false;
    public bool estaEnAreaDefensa = false;
    public bool hayMagoCercaConHueco = false;
    public bool estoyEnUnEquipo = false;
    public Transform magoConMenosAliados;
    public List <GameObject> [] magosDisponibles;

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
        Debug.Log(playerEnRangoVision);

        if (EstoyEnEquipo())
        {   
            Debug.Log("Estoy en equipo");
            if (EstoyEnAreaDefensa())
            {
            Debug.Log("Estoy en areaDefensa");

                if (playerEnRangoVision)
                {
            Debug.Log("Estoy en AreaVision");

                    if (playerEnRangoPegar)
                        Pegar();
                    else
                        Perseguir();
                }
                else
                {
                    AcercarseAlPlayer();
            Debug.Log("Me acerco al player");

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
    SphereCollider sphCol = rangoDeVision.GetComponent<SphereCollider>(); // Pillo el componente para poder calcular el area de vision
    float radio = sphCol.radius * rangoDeVision.transform.lossyScale.x; // lo convierto en float
    return Physics.CheckSphere(transform.position, radio, whatIsPlayer);
    //Cambiar el radio de la sphera por raycast como el MagoBehaviour
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
    int menorCantidad = int.MaxValue;
    MageBehaviour mejorMago = null;

    GameObject[] magos = GameObject.FindGameObjectsWithTag("Mago");

    foreach (GameObject magoObj in magos)
    {
        MageBehaviour mago = magoObj.GetComponent<MageBehaviour>();
        if (mago == null) continue;

        int cantidadActual = 0;

        foreach (GameObject miembro in mago.teamMembers)
        {
            if (miembro != null)
                cantidadActual++;
        }

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
                mejorMago.teamMembers[i] = gameObject; // este minion se asigna
                targetToFollow = mejorMago.transform;
                estoyEnUnEquipo = true;
                estaEnEquipo = true;
                magoConMenosAliados = mejorMago.transform;

                Debug.Log("Asignado al mago: " + mejorMago.name);
                break;
            }
        }
    }
    else
    {
        Debug.Log("No hay magos con hueco.");
    }
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
