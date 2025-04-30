using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class MenuSystem : MonoBehaviour
{
    public GameObject playButton;
    public GameObject optionsPanel;
    public GameObject creditosPanel;
    public GameObject confirmarPanel;



    void Start()
    {
        SelecionMando(playButton);
    }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Opciones()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);

    }

    public void Creditos()
    {
        creditosPanel.SetActive(!creditosPanel.activeSelf);
    }

    public void Confirmar()
    {
        confirmarPanel.SetActive(!confirmarPanel.activeSelf);
    }

    public void Volver(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void SelecionMando(GameObject selectedButton)
    {
        // Debug.Log("Seleccionando el botón: " + selectedButton.name);
        EventSystem.current.SetSelectedGameObject(selectedButton);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
