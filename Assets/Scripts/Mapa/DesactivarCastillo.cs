using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarCastillo : MonoBehaviour
{
    public GameObject castillo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Desactivar el castillo
            castillo.SetActive(false);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reactivar el castillo si es necesario
            castillo.SetActive(true);
        }
    }
}
