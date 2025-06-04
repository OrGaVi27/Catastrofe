using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaBoss : MonoBehaviour
{
    public GameObject guardado;
    [Space]
    public GameObject puertaAbierta;
    public GameObject puertaCerrada;
    [Space]
    public GameObject fireObject;
    public GameObject waterObject;
    public GameObject electricityObject;
    public GameObject rockObject;
    [Space]
    public Material[] materials;

    void Start()
    {
        if (guardado.GetComponent<GuardarPartida>().datosGuardado.fire == true &&
            guardado.GetComponent<GuardarPartida>().datosGuardado.water == true &&
            guardado.GetComponent<GuardarPartida>().datosGuardado.electricity == true &&
            guardado.GetComponent<GuardarPartida>().datosGuardado.rock == true)
        {
            puertaAbierta.SetActive(true);
            puertaCerrada.SetActive(false);
        }
        else
        {
            puertaAbierta.SetActive(false);
            puertaCerrada.SetActive(true);
        }

        if (guardado.GetComponent<GuardarPartida>().datosGuardado.fire == true)
        {
            fireObject.GetComponent<Renderer>().material = materials[1];
        }
        else
        {
            fireObject.GetComponent<Renderer>().material = materials[0];
        }

        if (guardado.GetComponent<GuardarPartida>().datosGuardado.water == true)
        {
            waterObject.GetComponent<Renderer>().material = materials[2];
        }
        else
        {
            waterObject.GetComponent<Renderer>().material = materials[0];
        }

        if (guardado.GetComponent<GuardarPartida>().datosGuardado.electricity == true)
        {
            electricityObject.GetComponent<Renderer>().material = materials[3];
        }
        else
        {
            electricityObject.GetComponent<Renderer>().material = materials[0];
        }
        
        if(guardado.GetComponent<GuardarPartida>().datosGuardado.rock == true)
        {
            rockObject.GetComponent<Renderer>().material = materials[4];
        }
        else
        {
            rockObject.GetComponent<Renderer>().material = materials[0];
        }
    }

    void OnEnable()
    {
        if (guardado.GetComponent<GuardarPartida>().datosGuardado.fire == true &&
            guardado.GetComponent<GuardarPartida>().datosGuardado.water == true &&
            guardado.GetComponent<GuardarPartida>().datosGuardado.electricity == true &&
            guardado.GetComponent<GuardarPartida>().datosGuardado.rock == true)
        {
            puertaAbierta.SetActive(true);
            puertaCerrada.SetActive(false);
        }
        else
        {
            puertaAbierta.SetActive(false);
            puertaCerrada.SetActive(true);
        }

        if (guardado.GetComponent<GuardarPartida>().datosGuardado.fire == true)
        {
            fireObject.GetComponent<Renderer>().material = materials[1];
        }
        else
        {
            fireObject.GetComponent<Renderer>().material = materials[0];
        }

        if (guardado.GetComponent<GuardarPartida>().datosGuardado.water == true)
        {
            waterObject.GetComponent<Renderer>().material = materials[2];
        }
        else
        {
            waterObject.GetComponent<Renderer>().material = materials[0];
        }

        if (guardado.GetComponent<GuardarPartida>().datosGuardado.electricity == true)
        {
            electricityObject.GetComponent<Renderer>().material = materials[3];
        }
        else
        {
            electricityObject.GetComponent<Renderer>().material = materials[0];
        }

        if (guardado.GetComponent<GuardarPartida>().datosGuardado.rock == true)
        {
            rockObject.GetComponent<Renderer>().material = materials[4];
        }
        else
        {
            rockObject.GetComponent<Renderer>().material = materials[0];
        }
    }
}
