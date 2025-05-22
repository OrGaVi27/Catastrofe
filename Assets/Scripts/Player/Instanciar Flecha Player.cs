using System;
using UnityEngine;

public class InstanciarFlechaPlayer : MonoBehaviour
{
    public GameObject flechaPrefab;
    public GameObject puntoSpawn;

    public void InstanciarFlecha()
    {
        GameObject flecha = Instantiate(flechaPrefab, puntoSpawn.transform.position, Quaternion.identity);
    }
}
