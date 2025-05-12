using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausaPanel;
    public GameObject volverAlJuego;
    public GameObject confirmarPanel;
    public GameObject diraio;
    public GameObject optionsPanel;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Keyboard.current.escapeKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.JoystickButton7)) && pausaPanel.activeSelf == false)
        {
            Pausa();
        }
    }

    public void Pausa()
    {
        Debug.Log("Pausa activada");
        pausaPanel.SetActive(!pausaPanel.activeSelf);
        if (pausaPanel == true)
        {
            SelecionMando(volverAlJuego);
        }

        Time.timeScale = pausaPanel.activeSelf ? 0 : 1;
    }

    public void Confirmar()
    {
        confirmarPanel.SetActive(!confirmarPanel.activeSelf);
    }

    public void Diario()
    {
        diraio.SetActive(!diraio.activeSelf);
    }

    public void Opciones()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }

    public void SelecionMando(GameObject selectedButton)
    {
        // Debug.Log("Seleccionando el botón: " + selectedButton.name);
        EventSystem.current.SetSelectedGameObject(selectedButton);
    }

    public void DesactivarEscena(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // void OnPausa()
    // {
    //     Debug.Log("Pausa activada");
    //     pausaPanel.SetActive(!pausaPanel.activeSelf);
    //     SelecionMando(volverAlJuego);
    //     Time.timeScale = pausaPanel.activeSelf ? 0 : 1;
    // }
}
