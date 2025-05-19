using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<string> objetosClave = new List<string>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(string nombreObjet)
    {
        if (!objetosClave.Contains(nombreObjet))
        {
            objetosClave.Add(nombreObjet);
            Debug.Log("Item añadido: " + nombreObjet);
        }
    }
}
