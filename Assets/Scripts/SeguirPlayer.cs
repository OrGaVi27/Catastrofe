using UnityEngine;
using UnityEngine.AI;

public class SeguirPlayer : MonoBehaviour
{
    public NavMeshAgent agent;  // El componente NavMeshAgent que se encarga de la navegación del enemigo.
    public Transform player;  // El objeto "Player" (jugador) al que el enemigo debe seguir.
    public LayerMask whatIsPlayer, whatIsGround;  // Capa para identificar al jugador y al terreno.

    // Parámetros de patrullaje
    public float timeGuard = 5f;  // Tiempo que el enemigo espera en cada waypoint antes de moverse.
    public Transform[] waypoint;  // Array de puntos de patrullaje (waypoints).
    private int indiceWaypoint = 0;  // Índice del waypoint actual.
    private float timer = 0f;  // Temporizador para contar el tiempo en cada waypoint.
    private bool playerEnRangoDeVision, playerEnRangoDeAtaque;  // Bandera para saber si el jugador está en el rango de visión o de ataque.
    public float rangoDeVision, rangoDeAtaque;  // Rango de visión y de ataque del enemigo.
    bool alreadyAttacked;  // Para controlar que no se ataque varias veces sin esperar.

    // Start is called before the first frame update
    private void Awake()
    {
        // Inicializamos el jugador y el agente de navegación al inicio
        player = GameObject.Find("Player").transform;  // Encontramos el objeto del jugador en la escena.
        agent = GetComponent<NavMeshAgent>();  // Obtenemos el componente NavMeshAgent del enemigo.
    }

    // Update se ejecuta cada frame
    void Update()
    {
        // Comprobamos si el jugador está en el rango de visión o de ataque.
        playerEnRangoDeVision = Physics.CheckSphere(transform.position, rangoDeVision, whatIsPlayer);
        playerEnRangoDeAtaque = Physics.CheckSphere(transform.position, rangoDeAtaque, whatIsPlayer);

        // Si el jugador no está en rango de visión ni de ataque, el enemigo patrulla.
        if (!playerEnRangoDeVision && !playerEnRangoDeAtaque)
        {
            Patrullaje();  // Llamamos a la función que gestiona el patrullaje.
        }

        // Si el jugador está en rango de visión pero fuera de rango de ataque, el enemigo lo persigue.
        if (playerEnRangoDeVision && !playerEnRangoDeAtaque)
        {
            chasePlayer();
        }

        // Si el jugador está dentro del rango de visión y de ataque, el enemigo lo ataca.
        if (playerEnRangoDeAtaque && playerEnRangoDeVision)
        {
            if(playerEnRangoDeVision)
            {
                Debug.Log("Jugador en rango de ataque!");  // Mensaje de depuración para indicar que el jugador está en rango de ataque.
            }
            else
            {
                Debug.Log("Jugador fuera de rango de ataque!");  // Mensaje de depuración para indicar que el jugador está fuera de rango de ataque.
            }
            // Si el jugador está en rango de ataque, llamamos a la función de ataque.
            attackPlayer();
        }
    }

    // Función que maneja el patrullaje del enemigo
    private void Patrullaje()
    {
        // Si el enemigo ha llegado al waypoint actual
        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            timer += Time.deltaTime;  // Contamos el tiempo que el enemigo ha estado en el waypoint actual.

            // Si el tiempo en el waypoint es mayor o igual al tiempo guardado, cambiamos al siguiente waypoint
            if (timer >= timeGuard)
            {
                timer = 0f;  // Reseteamos el temporizador.
                indiceWaypoint = (indiceWaypoint + 1) % waypoint.Length;  // Vamos al siguiente waypoint (circular).
                agent.SetDestination(waypoint[indiceWaypoint].position);  // Establecemos el siguiente waypoint como destino.
            }
        }
        else
        {
            // Si el enemigo aún no ha llegado al waypoint, continúa con el destino actual.
            agent.SetDestination(waypoint[indiceWaypoint].position);
        }
    }

    // Función que maneja el comportamiento de perseguir al jugador.
    private void chasePlayer()
    {
        agent.SetDestination(player.position);  // Establecemos la posición del jugador como destino.
    }

    // Función que maneja el ataque al jugador.
    private void attackPlayer()
    {
        agent.SetDestination(transform.position);  // Detenemos al enemigo al llegar al jugador.
        transform.LookAt(player);  // El enemigo mira al jugador.

        // Si el enemigo no ha atacado aún, realizamos el ataque.
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;  // Marcamos que el enemigo ya ha atacado.
            Debug.Log("Atacando al jugador!");  // Mensaje de depuración para indicar que se ha atacado.
            Invoke(nameof(ResetAttack), 1f);  // Llamamos a ResetAttack después de un segundo.
        }
    }

    // Función para resetear el estado de ataque después de un tiempo.
    private void ResetAttack()
    {
        alreadyAttacked = false;  // Permitimos que el enemigo ataque nuevamente.
    }
}
