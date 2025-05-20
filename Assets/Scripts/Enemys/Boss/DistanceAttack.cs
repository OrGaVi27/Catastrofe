using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DistanceAttack : MonoBehaviour
{
    public bool distanceAttack;

    public MeleeAtack melee;
    
    public void Start()
    {
        distanceAttack = false;

        melee = FindAnyObjectByType<MeleeAtack>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            distanceAttack = true;

            /* Debug.Log("distance"); */
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            distanceAttack = false;

            /* Debug.Log("NoDistance"); */
        }
    }
}
