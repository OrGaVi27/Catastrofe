using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerTutorial : MonoBehaviour
{

    public GameObject ventana;
    public GameObject selectedButton;
    public Animator animator;
    public bool isPopUp;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SelecionMando(selectedButton);
            if (animator != null)
            {
                animator.SetBool("Out", true);
            }
            else
            {
                ventana.SetActive(ventana.activeSelf ? false : true);
                this.gameObject.SetActive(false);

            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("1Trigger Entered by: " + other.name);
        if (other.CompareTag("Player") && !isPopUp)
        {
            Debug.Log("2Trigger Entered by: " + other.name);
            SelecionMando(selectedButton);
            if (animator != null)
            {
                ventana.SetActive(true);
            }
        }
    }

    public void SelecionMando(GameObject selectedButton)
    {
        // Debug.Log("Seleccionando el botón: " + selectedButton.name);
        EventSystem.current.SetSelectedGameObject(selectedButton);
    }

    public void Desactivar()
    {
        this.gameObject.SetActive(false);

    }
}
