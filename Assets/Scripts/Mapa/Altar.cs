using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public GameObject habitacion;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            

            Debug.Log("a");
        }
    }
}
