using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
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
            if (other.tag == "Player")
            {
                //Debug.Log("Impacto Jugador");
                player.GetComponent<BasePlayerStats>().TakeDamage(damage); // Llama a la función de daño del jugador

            }
            //Debug.Log("Flecha colisiona con " + other.tag);
            Destroy(gameObject);
        }

    }
}
