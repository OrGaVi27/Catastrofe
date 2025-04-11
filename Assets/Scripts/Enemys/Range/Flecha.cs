using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public GameObject player;
    public float damage;
    public float tiempoDeVidaTotal = 3f;
    private float currentDelateTime;
    public float velocidadFlecha = 10f;
    private Vector3 direccion;
    private Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentDelateTime = 0;

        direccion = (player.transform.position - transform.position).normalized;
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
        if (other.tag == "Player")
        {
            // Debug.Log("Flecha colisiona con el jugador");
            player.GetComponent<BasePlayerStats>().TakeDamage(damage); // Llama a la función de daño del jugador
        }



        if (other.tag == "Player" || other.tag == "Ground" || other.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }
}
