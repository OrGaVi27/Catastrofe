using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarCastillo : MonoBehaviour
{

    public GameObject castillo;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            castillo.SetActive(true);
        }
    }
}
