using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FaceDilateAnimEditor : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float dilateSlider;

    // Update is called once per frame
    void Update()
    {
        var tmp = gameObject.GetComponent<TMP_Text>();
        if (tmp != null && tmp.fontMaterial != null)
        {
            tmp.fontMaterial.SetFloat("_FaceDilate", dilateSlider);
        }
    }
}
