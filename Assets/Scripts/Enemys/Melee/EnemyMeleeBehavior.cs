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

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        minionMele = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (estoyEnUnEquipo)
        {
            if (EstoyEnAreaDeDefensa())
            {
                if (PlayerEnRangoVision())
                {
                    if (PlayerEnRangoDePegar())
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
                PosicionarseEnAreaDefensa();
            }
        }
        else
        {
            if (HayMagoDisponibleCercano())
            {
                AsignarseAMagoConMenosAliados();
            }
            else
            {
                if (PlayerEnRangoVision())
                {
                    if (PlayerEnRangoDePegar())
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
        minionMele.SetDestination(player.position);
        Debug.Log("Persiguiend Player");
    }

    void Pegar()
    {
        minionMele.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Debug.Log("¡Atacando al jugador!");
            Invoke(nameof(ResetAttack), 1.5f);
        }
    }

    void SeguirAliado()
    {
        if (targetToFollow != null)
        {
            Debug.Log("Acompañando Aliado");
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
