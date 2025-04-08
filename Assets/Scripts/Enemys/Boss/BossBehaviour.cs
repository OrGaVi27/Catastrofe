using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    public bool fire;
    public bool water;
    public bool electricity;
    public bool rock;

    public List<string> activeElements = new List<string>();

    public Slider fireSlider;
    public Slider waterSlider;
    public Slider electricitySlider;
    public Slider rockSlider;

    // Objeto padre donde se moverán los sliders
    public Transform slidersParent;

    void Update()
    {
        
    }

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

    void UpdateHierarchyOrder()
    {
        // Primero, aseguramos que la lista activeElements está ordenada
        int index = 0;

        // Reorganizamos los sliders en la jerarquía del objeto padre según el orden de activeElements
        foreach (string element in activeElements)
        {
            switch (element)
            {
                case "Fire":
                    fireSlider.transform.SetSiblingIndex(index++);
                    break;
                case "Water":
                    waterSlider.transform.SetSiblingIndex(index++);
                    break;
                case "Electricity":
                    electricitySlider.transform.SetSiblingIndex(index++);
                    break;
                case "Rock":
                    rockSlider.transform.SetSiblingIndex(index++);
                    break;
            }
        }
    }
    
    void OnEnable()
    {
        UpdateActiveElements();

        UpdateHierarchyOrder();
    }
}
