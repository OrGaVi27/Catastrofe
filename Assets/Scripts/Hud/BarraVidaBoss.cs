using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaBoss : MonoBehaviour
{
    public BossBehaviour enemy;

    [Header("Sliders")]
    public Slider healthSlider;
    public Slider easeHealthSlider;

    [Header("Stats")]
    public BaseEnemyStats enemyStats;
    public float lerpSpeed = 0.05f;
    void Start()
    {
        enemy = FindAnyObjectByType<BossBehaviour>();

        enemy.currentLiife = enemy.maxLiife;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != enemy.currentLiife)
        {
            healthSlider.value = enemy.currentLiife;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, enemy.currentLiife, lerpSpeed);
        }

    }
}
