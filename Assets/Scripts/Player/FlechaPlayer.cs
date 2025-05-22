using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlechaPlayer : MonoBehaviour
{
    public GameObject player;
    public float tiempoDeVidaTotal;
    public float velocidadFlecha;
    public bool flechaEspecial;
    public GameObject ExplosionPrefab;

    private float damage;
    private float currentDeleteTime;
    private Vector3 direccion;
    private Rigidbody rb;

    int playerElement;
    float elementMult;


    void Start()
    {
        currentDeleteTime = 0;

        
        damage = player.GetComponent<PlayerStateManager>().DamageOutput();

        playerElement = player.GetComponent<PlayerStateManager>().element;
        elementMult = player.GetComponent<BasePlayerStats>().elementMultiplier;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentDeleteTime += Time.deltaTime;

        if (currentDeleteTime >= tiempoDeVidaTotal)
        {
            Destroy(gameObject);
        }

        //Velocidad hacia el jugador
        rb.velocity = direccion * velocidadFlecha;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseEnemyStats>() != null || other.tag == "Ground" || other.tag == "Wall")
        {
            if (other.GetComponent<BaseEnemyStats>() != null)
            {
                if (flechaEspecial)
                {
                    Instantiate(ExplosionPrefab, transform.position, Quaternion.identity); // Instancia la explosion Elemental
                }
                else
                {
                    other.GetComponent<BaseEnemyStats>().TakeDamage(damage, playerElement, elementMult); // Llama a la función de daño del enemigo
                }
                
            }


            Destroy(gameObject);
        }

    }
}
