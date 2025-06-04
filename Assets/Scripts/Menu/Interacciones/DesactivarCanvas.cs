using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarCanvas : MonoBehaviour
{
    public void Desactivar()
    {
        // Desactiva el canvas al que está asociado este script
        gameObject.SetActive(false);
    }
}
