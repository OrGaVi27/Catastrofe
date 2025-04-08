using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public float tiempoDeVidaTotal = 3f;
    private float currentDelateTime;
    void Start()
    {
        currentDelateTime = 0;
    }

    void Update()
    {
        currentDelateTime += Time.deltaTime;

        if (currentDelateTime >= tiempoDeVidaTotal)
        {
            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Ground" || other.tag == "Wall")
        {
            Destroy(gameObject);
        }

        Debug.Log(other.tag);
    }
}
