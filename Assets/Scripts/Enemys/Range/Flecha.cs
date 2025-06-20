using UnityEngine;

public class Flecha : MonoBehaviour
{
    public GameObject player;
    public float damage;
    public float tiempoDeVidaTotal;
    private float currentDelateTime;
    public float velocidadFlecha;
    private Vector3 direccion;
    private Rigidbody rb;
    protected int hits = 0;
    [SerializeField] private AudioClip disparar;


    void Start()
    {
            ControladorSonido.Instance.EjecutarSonido(disparar);

        player = GameObject.FindGameObjectWithTag("Player");
        currentDelateTime = 0;

        direccion = (player.transform.GetChild(0).position - transform.position).normalized;

        // Ajustar la rotación de la flecha para que el eje X sea 0
        Quaternion rotacionActual = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Euler(0, rotacionActual.eulerAngles.y, rotacionActual.eulerAngles.z);

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentDelateTime += Time.deltaTime;

        if (currentDelateTime >= tiempoDeVidaTotal)
        {
            Destroy(gameObject);
        }

        //Velocidad hacia el jugador
        rb.velocity = direccion * velocidadFlecha;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Ground" || other.tag == "Wall")
        {
            if (other.tag == "Player"   && hits == 0)
            {
                hits++;
                //Debug.Log("Flecha colisiona con " + other.tag);
                //Debug.Log("Impacto Jugador");
                player.GetComponent<BasePlayerStats>().TakeDamage(damage); // Llama a la función de daño del jugador
            }
            //Debug.Log("Flecha colisiona con " + other.tag);
            Destroy(gameObject);
        }

    }
}
