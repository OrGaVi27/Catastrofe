using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtack : MonoBehaviour
{
    public bool meleeAtack;

    public DistanceAttack distance;

    public void Start()
    {
        meleeAtack = false;

        distance = FindAnyObjectByType<DistanceAttack>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meleeAtack = true;
            distance.distanceAttack = false;

            /* Debug.Log("melee");
            Debug.Log("NoDistance"); */
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meleeAtack = false;
            distance.distanceAttack = true;

            /* Debug.Log("NoMelee");
            Debug.Log("Distance"); */
        }
    }
}
