using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyVisionArea : MonoBehaviour
{
    public PlayerStateManager player;
    public bool playerInRange = false;
    public bool magesInRange = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        if(player.baseStats.currentHealth <= 0f)
        {
            playerInRange = false;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
        if (other.CompareTag("Mago"))
        {
            magesInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
        if (other.CompareTag("Mago"))
        {
            magesInRange = false;
        }
    }


}