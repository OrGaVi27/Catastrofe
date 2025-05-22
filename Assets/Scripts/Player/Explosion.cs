using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public void SetElement(int element)
    {
        switch (element)
        {
            case 0:
                GetComponent<ParticleSystem>().startColor = new Color(255, 255, 255);
                break;

            case 1:
                GetComponent<ParticleSystem>().startColor = new Color(255, 0, 0);
                break;

            case 2:
                GetComponent<ParticleSystem>().startColor = new Color(214, 69, 12);
                break;

            case 3:
                GetComponent<ParticleSystem>().startColor = new Color(255, 251, 0);
                break;

            case 4:
                GetComponent<ParticleSystem>().startColor = new Color(0, 21, 255);
                break;
        }
        
    }
}
