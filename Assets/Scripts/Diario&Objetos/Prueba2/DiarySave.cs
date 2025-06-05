using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiarySave : MonoBehaviour
{
    public GameObject guardado;
    public int idPagina;

    void Start()
    {
         Debug.Log("ID de la página: " + idPagina);
        if (idPagina >= 1 && idPagina <= 5)
        {
            int index = idPagina - 1;
            if (guardado.GetComponent<GuardarPartida>().datosGuardado.paginasDiario[index])
            {
                gameObject.SetActive(false);
            Debug.LogWarning("idPagina : " + idPagina);

            }
        }
        else
        {
            Debug.LogWarning("idPagina está fuera de rango: " + idPagina);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (idPagina >= 1 && idPagina <= 5)
            {
                int index = idPagina - 1;
                guardado.GetComponent<GuardarPartida>().datosGuardado.paginasDiario[index] = true;
                if (idPagina == 1)
                {
                    DesactivarTuto.instance.DesactivarTutorial();
                }
                guardado.GetComponent<GuardarPartida>().GuardarJSON();
            }
            else
            {
                Debug.LogWarning("idPagina está fuera de rango: " + idPagina);
            }

            Destroy(gameObject);
        }
    }


}
