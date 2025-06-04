using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuertaPuzleOrbe : MonoBehaviour
{
    public AbrirPuertaAbajo puerta;

    void Start()
    {
        // Inicialización si es necesario
         puerta = GetComponent<AbrirPuertaAbajo>(); ;
    }

    public void AbrirPuerta()
    {
        puerta.isOpen = true;
    }
}
