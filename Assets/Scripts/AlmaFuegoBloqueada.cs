using UnityEngine;

public class AlmaFuegoBloqueada : MonoBehaviour
{
    public GameObject guardado;
    public GameObject gema;

    void Update()
    {
        if (guardado.GetComponent<GuardarPartida>().datosGuardado.espada01 == true &&
        guardado.GetComponent<GuardarPartida>().datosGuardado.espada02 == true &&
        guardado.GetComponent<GuardarPartida>().datosGuardado.espada03 == true &&
        guardado.GetComponent<GuardarPartida>().datosGuardado.fire == false )
        {
            gema.SetActive(true);
        }
        else
        {
            gema.SetActive(false);
        }
    }
}
