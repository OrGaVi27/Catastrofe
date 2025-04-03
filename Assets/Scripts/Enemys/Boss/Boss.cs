using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Element
{
    Fire,
    Water,
    Rock,
    Electricity
}

[Serializable]
public class LifeChunk
{
    public float value = 100;
    public Element element = new Element();
}

public class Boss : BaseEnemyStats
{
    [Header("Vidas")]
    public LifeChunk[] life;
    public float maxLife = 100;

    void Start()
    {

    }

    void Update()
    {
        
    }
}
