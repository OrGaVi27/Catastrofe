using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrozoEspada : MonoBehaviour
{
    public GameObject guardado;
    [Space]
    public bool espada01;
    public bool espada02;
    public bool espada03;
    [SerializeField] public AudioClip PickUpSound;


    void Start()
    {
        if (espada01 == true && guardado.GetComponent<GuardarPartida>().datosGuardado.espada01 == true)
        {
            gameObject.SetActive(false);
        }

        if (espada02 == true && guardado.GetComponent<GuardarPartida>().datosGuardado.espada02 == true)
        {
            gameObject.SetActive(false);
        }

        if (espada03 == true && guardado.GetComponent<GuardarPartida>().datosGuardado.espada03 == true)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (espada01)
            {
                guardado.GetComponent<GuardarPartida>().datosGuardado.espada01 = true;
            }

            if (espada02)
            {
                guardado.GetComponent<GuardarPartida>().datosGuardado.espada02 = true;
            }

            if (espada03)
            {
                guardado.GetComponent<GuardarPartida>().datosGuardado.espada03 = true;
            }

            guardado.GetComponent<GuardarPartida>().GuardarJSON();
            ControladorSonido.Instance.EjecutarSonido(PickUpSound);  

            Destroy(gameObject);
        }
    }
}
