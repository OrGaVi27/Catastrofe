using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarradevidaEnemigos : MonoBehaviour
{
    public GameObject enemy;

    [Header("Sliders")]
    public Slider healthSlider;
    public Slider easeHealthSlider;

    [Header("Stats")]
    public BaseEnemyStats enemyStats;
    public float lerpSpeed = 0.05f;
    void Start()
    {
        enemyStats = enemy.GetComponent<BaseEnemyStats>();

        enemyStats.currentHealth = enemyStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != enemyStats.currentHealth)
        {
            healthSlider.value = enemyStats.currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, enemyStats.currentHealth, lerpSpeed);
        }

    }
}
