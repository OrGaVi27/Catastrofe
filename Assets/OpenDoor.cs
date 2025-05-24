using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Door.transform.GetChild(0).gameObject.SetActive(true);
            Door.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
