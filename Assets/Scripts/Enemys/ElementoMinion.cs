using UnityEngine;
using UnityEngine.UI;


public class ElementoMinion : MonoBehaviour
{
    public GameObject minion;

    public int elemento = 0;

    public GameObject parte1;
    public GameObject parte2;
    public Material[] materialesElementos;


    public Sprite[] iconosElementos;
    public Image iconoElemento;
    public Image Vida;
    public Image Vida2;

    Color rojoPersonalizado;
    Color marronPersonalizado;
    Color azulPersonalizado;
    Color amarilloPersonalizado;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ColorUtility.TryParseHtmlString("#EF4A4A", out rojoPersonalizado);
        ColorUtility.TryParseHtmlString("#B07C66", out marronPersonalizado);
        ColorUtility.TryParseHtmlString("#BDBB6C", out amarilloPersonalizado);
        ColorUtility.TryParseHtmlString("#525FEF", out azulPersonalizado);

    }

    // Update is called once per frame
    void Update()
    {
        if (elemento == 0)
        {
            // Debug.Log("El Minion no tiene elemento");
            iconoElemento.sprite = null;
            iconoElemento.color = Color.clear;
            Vida.color = Color.white;
            Vida2.color = Color.white;

        }
        else if (elemento == 1)
        {
            // Debug.Log("El Minion es de fuego");
            iconoElemento.sprite = iconosElementos[0];
            iconoElemento.color = Color.white;
            Vida.color = rojoPersonalizado;
            Vida2.color = rojoPersonalizado;



        }
        else if (elemento == 4)
        {
            // Debug.Log("El Minion es de agua");
            iconoElemento.sprite = iconosElementos[3];
            iconoElemento.color = Color.white;
            Vida.color = azulPersonalizado;
            Vida2.color = azulPersonalizado;


        }
        else if (elemento == 2)
        {
            // Debug.Log("El Minion es de roca");
            iconoElemento.sprite = iconosElementos[1];
            iconoElemento.color = Color.white;
            Vida.color = marronPersonalizado;
            Vida2.color = marronPersonalizado;


        }
        else if (elemento == 3)
        {
            // Debug.Log("El Minion es de electricidad");
            iconoElemento.sprite = iconosElementos[2];
            iconoElemento.color = Color.white;
            Vida.color = amarilloPersonalizado;
            Vida2.color = amarilloPersonalizado;


        }
    }

    void OnTriggerStay(Collider other)
    {

        if (minion.GetComponent<BaseEnemyStats>() != null)
        {
            if (minion.GetComponent<BaseEnemyStats>().dead) elemento = 0;
        }


        if (elemento == 0 && other.gameObject.tag == "EnemigoElemental")
        {
            // Debug.Log("El Minion ha tocado a un enemigo elemental");
            EnemigoElemental enemigo = other.GetComponent<EnemigoElemental>();

            if (enemigo.fuego)
            {
                elemento = 1;
            }
            else if (enemigo.agua)
            {
                elemento = 4;
            }
            else if (enemigo.roca)
            {
                elemento = 2;
            }
            else if (enemigo.electricidad)
            {
                elemento = 3;
            }

            parte1.GetComponent<Renderer>().material = materialesElementos[elemento];
            parte2.GetComponent<Renderer>().material = materialesElementos[elemento];
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemigoElemental")
        {
            elemento = 0;
            parte1.GetComponent<Renderer>().material = materialesElementos[elemento];
            parte2.GetComponent<Renderer>().material = materialesElementos[elemento];
        }
    }
}
