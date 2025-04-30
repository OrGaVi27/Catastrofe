using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarradeMana : MonoBehaviour
{
    public GameObject player;
    public Image[] manaSrpite; // Array de sprites para los diferentes elementos

    [Header("Stats")]
    public BasePlayerStats playerStats;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<BasePlayerStats>();

        playerStats.currentMana = playerStats.maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerStats.currentMana)
        {
            case 0:
                // Debug.Log("No tienes mana");
                foreach (var mana in manaSrpite)
                {
                    mana.color = Color.clear;
                }
                break;

            default:
                // Debug.Log($"Tienes {playerStats.currentMana} de mana");
                for (int i = 0; i < manaSrpite.Length; i++)
                {
                    if (i < playerStats.currentMana)
                    {
                        manaSrpite[i].color = Color.white; // Mostrar los sprites correspondientes al mana disponible
                    }
                    else
                    {
                        manaSrpite[i].color = Color.clear; // Ocultar los sprites restantes
                    }
                }
                break;
        }
    }
}
