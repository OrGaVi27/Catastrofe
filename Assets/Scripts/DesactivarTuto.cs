using UnityEngine;

public class DesactivarTuto : MonoBehaviour
{
    public static DesactivarTuto instance;
    public GameObject tutorial;
    public GameObject enemigos;
    public GameObject guardado;

    void Start()
    {
        if (guardado.GetComponent<GuardarPartida>().datosGuardado.tutorial == false)
            {
                DesactivarTutorial();
            }
    }
    public void DesactivarTutorial()
    {
        tutorial.SetActive(false);
        enemigos.SetActive(true);

        if (guardado.GetComponent<GuardarPartida>().datosGuardado.tutorial == true)
        {
            guardado.GetComponent<GuardarPartida>().datosGuardado.tutorial = false;
        }
    }
    

    void Awake()
    {
        if (instance == null) instance = this;
    }

}