using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitacionesController : MonoBehaviour
{
    public static HabitacionesController instance;
    public GameObject[] habitaciones;

    void Awake()
    {
        instance = this;
    }

    public void Cargado(string habitacionCargada)
    {
        foreach (var habitacion in habitaciones)
        {
            if (habitacion.name == habitacionCargada)
            {
                habitacion.SetActive(true);
            }
        }
    }
}
