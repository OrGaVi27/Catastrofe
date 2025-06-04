using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarDesactivar : MonoBehaviour
{
    public GameObject trigger;

    public void ActivarMensaje()
    {
        this.gameObject.SetActive(true);
        trigger.SetActive(true);
    }

    public void DesactivarMensaje()
    {
        this.gameObject.SetActive(false);
        trigger.SetActive(false);

    }
}
