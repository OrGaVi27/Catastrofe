using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarCamara : MonoBehaviour
{
    public GameObject camara;

    // Start is called before the first frame update
    void Start()
    {
        camara = GameObject.FindGameObjectWithTag("LookAt");

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camara.transform.position);
    }
}
