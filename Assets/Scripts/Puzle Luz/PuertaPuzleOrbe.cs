using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuertaPuzleOrbe : MonoBehaviour
{
    public AbrirPuertaAbajo puerta;

    void Start()
    {
         puerta = GetComponent<AbrirPuertaAbajo>(); ;
    }

    public void AbrirPuerta()
    {
        puerta.isOpen = true;
    }
}
