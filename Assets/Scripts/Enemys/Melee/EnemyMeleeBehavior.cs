using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeBehavior : BaseEnemyStats
{
    public NavMeshAgent minionMele;
    public Transform player;
    public Transform targetToFollow;
    public LayerMask whatIsPlayer, whatIsGround;

    public EnemyVisionArea rangoDeVision;
    public EnemyVisionArea rangoDeAtaque;

    public bool estaEnEquipo = false;
    public bool estoyEnUnEquipo = false;

    private bool alreadyAttacked;
    private float engagementRange = 1f;
    private float followRange = 2f;
    [SerializeField] public AudioClip atackSound;



    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        minionMele = GetComponent<NavMeshAgent>();
        nmAgent.speed = movementSpeed;
    }

    void Update()
    {
        if (dead)
        {
            GetComponent<Collider>().enabled = false;
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

        if (estoyEnUnEquipo)
        {
            if (targetToFollow.GetComponent<BaseEnemyStats>().dead)
            {
                GetComponent<ElementoMinion>().elemento = 0;
                estoyEnUnEquipo = false;
            }
        }


        //Se usa para calcular el daño de los ataques
        element = GetComponent<ElementoMinion>().elemento;

        if (estoyEnUnEquipo)
        {
            nmAgent.stoppingDistance = followRange;
            if (PlayerEnRangoVision())
            {
                nmAgent.stoppingDistance = engagementRange;
                if (PlayerEnRangoDePegar())
                {
                    Pegar();
                }
                else
                {
                    Perseguir();
                }

            }
            else
            {
                SeguirAliado();
            }

        }
        else
        {
            nmAgent.stoppingDistance = engagementRange;
            if (HayMagoDisponibleCercano())
            {
                AsignarseAMagoConMenosAliados();
            }
            else
            {
                if (PlayerEnRangoVision())
                {
                    if (PlayerEnRangoDePegar())
                    {
                        Perseguir();
                        Pegar();
                    }
                    else
                    {
                        Perseguir();

                    }
                }
                else
                {
                    SeguirAliado();
                }
            }
        }

        if (minionMele.velocity != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);

        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    bool PlayerEnRangoVision()
    {
        return rangoDeVision.playerInRange;
    }

    bool PlayerEnRangoDePegar()
    {
        return rangoDeAtaque.playerInRange;
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
        minionMele.SetDestination(player.position);
        //Debug.Log("Persiguiend Player");
    }

    void Pegar()
    {
        //minionMele.SetDestination(transform.position);
        //transform.LookAt(player);
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * movementSpeed);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Debug.Log("¡Atacando al jugador!");
            ControladorSonido.Instance.EjecutarSonido(atackSound);
            animator.SetTrigger("Attacking");
            Invoke(nameof(ResetAttack), 2f);
        }
    }

    void SeguirAliado()
    {
        if (targetToFollow != null)
        {
            //Debug.Log("Acompañando Aliado");
            minionMele.SetDestination(targetToFollow.position);
        }
    }

    void PosicionarseEnAreaDefensa()
    {
        if (targetToFollow != null)
        {
            Debug.Log("Estoy en posicion de area de defensa");
            minionMele.SetDestination(targetToFollow.position);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
