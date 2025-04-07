using UnityEngine;

public class ElementoMinion : MonoBehaviour
{
    public int elemento = 0;
    public Material[] materialesElementos;
    private new Renderer renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elemento == 0)
        {
            //Debug.Log("El Minion no tiene elemento");
        }
        else if (elemento == 1)
        {
            //Debug.Log("El Minion es de fuego");
        }
        else if (elemento == 2)
        {
            //Debug.Log("El Minion es de agua");
        }
        else if (elemento == 3)
        {
            //Debug.Log("El Minion es de roca");
        }
        else if (elemento == 4)
        {
            //Debug.Log("El Minion es de electricidad");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (elemento == 0 && other.gameObject.tag == "EnemigoElemental")
        {
            EnemigoElemental enemigo = other.GetComponent<EnemigoElemental>();

            if (enemigo.fuego)
            {
                elemento = 1;
            }
            else if (enemigo.agua)
            {
                elemento = 2;
            }
            else if (enemigo.roca)
            {
                elemento = 3;
            }
            else if (enemigo.electricidad)
            {
                elemento = 4;
            }

            renderer.material = materialesElementos[elemento];
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemigoElemental")
        {
            elemento = 0;
            renderer.material = materialesElementos[elemento];
        }
    }
}
