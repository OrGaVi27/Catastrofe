using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertaAbajo : MonoBehaviour
{
    public bool isOpen;

    public Animator anim;

    void Update()
    {
        if(isOpen == true)
        {
            anim.SetBool("isOpen", true);
        }
        else
        {
            anim.SetBool("isOpen", false);
        }
    }
    
    public void OnEnable()
    {
        isOpen = false;
    }
}
