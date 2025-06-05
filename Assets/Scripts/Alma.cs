using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alma : MonoBehaviour
{
    public GameObject guardado;
    [Space]
    public bool fire;
    public bool water;
    public bool electricity;
    public bool rock;

    void Start()
    {
        if (fire == true && guardado.GetComponent<GuardarPartida>().datosGuardado.fire == true)
        {
            gameObject.SetActive(false);
        }

        if (water == true && guardado.GetComponent<GuardarPartida>().datosGuardado.water == true)
        {
            gameObject.SetActive(false);
        }

        if (electricity == true && guardado.GetComponent<GuardarPartida>().datosGuardado.electricity == true)
        {
            gameObject.SetActive(false);
        }

        if (rock == true && guardado.GetComponent<GuardarPartida>().datosGuardado.rock == true)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fire)
            {
                guardado.GetComponent<GuardarPartida>().datosGuardado.fire = true;
            }

            if (water)
            {
                guardado.GetComponent<GuardarPartida>().datosGuardado.water = true;
            }

            if (electricity)
            {
                guardado.GetComponent<GuardarPartida>().datosGuardado.electricity = true;
            }

            if (rock)
            {
                guardado.GetComponent<GuardarPartida>().datosGuardado.rock = true;
            }

            guardado.GetComponent<GuardarPartida>().GuardarJSON();

            Destroy(gameObject);
        }
    }
}
