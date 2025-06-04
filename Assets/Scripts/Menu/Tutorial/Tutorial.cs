using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tutorial;
    public GameObject[] selectedButtons;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int contador = 0;
        foreach (GameObject ventana in tutorial)
        {
            if (ventana.activeSelf)
            {
            SelecionMando(selectedButtons[contador]);
            Pausa();
            return; // Salir del bucle si se activa la pausa
            }
            contador++;
        }
    }

    void Pausa()
    {
        Debug.Log("Pausa activada");
        // SelecionMando(botonSiguiente);
        Time.timeScale = 0; // Pausar el juego
    }

    public void DesactivarActual(GameObject actual)
    {
        Debug.Log("Siguiente");
        actual.SetActive(!actual.activeSelf);

    }

    public void ActivarSiguiente(GameObject siguiente)
    {
        siguiente.SetActive(!siguiente.activeSelf);
    }

    public void SelecionMando(GameObject selectedButton)
    {
        // Debug.Log("Seleccionando el botón: " + selectedButton.name);
        EventSystem.current.SetSelectedGameObject(selectedButton);
    }

    public void ReactivarTiempo()
    {
        Time.timeScale = 1;
    }


}
