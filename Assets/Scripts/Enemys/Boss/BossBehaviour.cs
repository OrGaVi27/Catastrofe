using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public int lifesCount;
    public Slider lifeSlider;

    public Image lifeColor;

    public float maxLiife;
    public float currentLiife;
    [Space]

    [Header("Pelea")]
    public float distanceCaC;
    public float distanciaMaxAttack;

    public Transform player;
    private float distancePlayer;

    void Update()
    {
        if (currentLiife <= 0)
        {
            if (lifesCount <= 4)
            {
                ChangeElement();

                //animacion de cabio de elemento
                lifesCount += 1;
                currentLiife = maxLiife;
            }
            else
            {
                //animacion morir Morir

                //eliminarme
            }
        }
        else
        {
            if (distancePlayer <= distanceCaC)
            {
                //ataque CaC
            }
            else if (distancePlayer >= distanceCaC && distancePlayer <= distanciaMaxAttack)
            {
                //Ataque a Distancia
            }
        }
    }

    void ChangeElement()
    {
        //pasar al siguiente elemnento de la lista de activeElements
    }

    void UpdateActiveElements()
    {
        if(fire /* && no esta en activeElements el fuego*/)
        {
            //añadir al ultimo puesto de activeElemets el fuego

            //eliminar el fuego de allElements
        }

        if (water /* && no esta en activeElements el water*/)
        {
            //añadir al ultimo puesto de activeElemets el water

            //eliminar el water de allElements
        }

        if (electricity /* && no esta en activeElements el electricity*/)
        {
            //añadir al ultimo puesto de activeElemets el electricity

            //eliminar el electricity de allElements
        }

        if (rock /* && no esta en activeElements el rock*/)
        {
            //añadir al ultimo puesto de activeElemets el rock

            //eliminar el rock de allElements
        }

    }
    
    void OnEnable()
    {
        UpdateActiveElements();

        currentLiife = maxLiife;
    }
}
