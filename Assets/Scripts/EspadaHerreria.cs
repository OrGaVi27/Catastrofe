using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaHerreria : MonoBehaviour
{
    public GameObject guardado;
    public Material[] materials;

    void OnEnable()
    {
        if (guardado.GetComponent<GuardarPartida>().datosGuardado.espada01 == true &&
        guardado.GetComponent<GuardarPartida>().datosGuardado.espada02 == true &&
        guardado.GetComponent<GuardarPartida>().datosGuardado.espada03 == true)
        {
            gameObject.GetComponent<Renderer>().material = materials[1];
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = materials[0];
        }
    }
}
