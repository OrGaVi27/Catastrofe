using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    public GameObject player;
    public float damage;
    public float tiempoDeVidaTotal;
    private float currentDelateTime;
    public float velocidadFlecha;
    private Rigidbody rb;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        currentDelateTime = 0;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentDelateTime += Time.deltaTime;

        if (currentDelateTime >= tiempoDeVidaTotal)
        {
            Destroy(gameObject);
        }

        transform.Translate(new Vector3(0, 0, velocidadFlecha) * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Ground" || other.tag == "Wall")
        {
            if (other.tag == "Player")
            {
                Debug.Log("Impacto Jugador");
                player.GetComponent<BasePlayerStats>().TakeDamage(damage); 
            }
            Debug.Log("Flecha colisiona con " + other.tag);
            Destroy(gameObject);
        }
    }
}
