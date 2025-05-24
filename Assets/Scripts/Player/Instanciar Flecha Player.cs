using System;
using Unity.VisualScripting;
using UnityEngine;

public class InstanciarFlechaPlayer : MonoBehaviour
{
    public GameObject flechaPrefab;
    public GameObject puntoSpawn;

    void Start()
    {
        puntoSpawn = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject;
    }

    public void InstanciarFlecha()
    {
        var flecha = Instantiate(flechaPrefab, puntoSpawn.transform.position, puntoSpawn.transform.rotation);
        var flechaPlayer = flecha.GetComponent<FlechaPlayer>();
        if (flechaPlayer != null)
        {
            flechaPlayer.GetComponent<FlechaPlayer>().player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
