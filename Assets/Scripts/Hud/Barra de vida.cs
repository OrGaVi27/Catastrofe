using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barradevida : MonoBehaviour
{
    [Header("Sliders")]
    public Slider healthSlider;
    public Slider easeHealthSlider;

    [Header("Stats")]
    public BasePlayerStats playerStats;
    public float lerpSpeed = 0.05f;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<BasePlayerStats>();
        
        playerStats.currentHealth = playerStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != playerStats.currentHealth)
        {
            healthSlider.value = playerStats.currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, playerStats.currentHealth, lerpSpeed);
        }

    }
}
