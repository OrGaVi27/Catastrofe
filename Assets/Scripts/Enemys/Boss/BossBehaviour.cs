using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    [Header("Elements")]
    public bool fire;
    public bool water;
    public bool electricity;
    public bool rock;

    public List<string> activeElements = new List<string>();
    public List<string> allElements = new List<string>();
    [Space]

    [Header("Life")]
    public Slider lifeSlider;

    public float maxLiife;
    public float currentLiife;
    [Header("Pelea")]
    public bool pelea;

    /*void Update()
    {
        if(currentLiife <= 0)
        {
            if (*//*si queda otro elemento en allElements a parte del current*//*)
            {
                //añadir element 0 de allElements a activeEmelemnts
                //quitar element 0 del allElemnts

                //animacion de cabio de elemento
                currentLiife = maxLiife;
            }
            else 
            { 
                //Morir
            }
        }
        else
        {
            if (*//*Distancia del Player <= a *//*)
            {

            }
        }
    }*/

    void UpdateActiveElements()
    {
        // Comprobar y agregar los elementos activos a la lista solo si no están ya en ella
        if (fire && !activeElements.Contains("Fire"))
            activeElements.Add("Fire");

        if (water && !activeElements.Contains("Water"))
            activeElements.Add("Water");

        if (electricity && !activeElements.Contains("Electricity"))
            activeElements.Add("Electricity");

        if (rock && !activeElements.Contains("Rock"))
            activeElements.Add("Rock");

        // Mostrar los elementos en la lista (solo los activos)
        foreach (var element in activeElements)
        {
            Debug.Log(element);
        }
    }
    
    void OnEnable()
    {
        UpdateActiveElements();
    }
}
