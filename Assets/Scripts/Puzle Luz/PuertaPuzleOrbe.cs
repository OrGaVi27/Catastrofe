using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuertaPuzleOrbe : MonoBehaviour
{
    public AbrirPuertaAbajo puerta;
    
    public void AbrirPuerta()
    {
        puerta.isOpen = true;
    }
}
