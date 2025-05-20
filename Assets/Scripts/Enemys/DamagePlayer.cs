using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public GameObject player;

    public float damage;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        gameObject.SetActive(false);
    }

    public void ActiveDamage()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.tag == "Player")
            {
                Debug.Log("Impacto Jugador");
                player.GetComponent<BasePlayerStats>().TakeDamage(damage); // Llama a la función de daño del jugador

            }
        }
    }
}
