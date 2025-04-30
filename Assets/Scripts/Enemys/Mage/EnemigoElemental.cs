using UnityEngine;
using System.Collections.Generic;

public class EnemigoElemental : MonoBehaviour
{
    [Header("Area")]
    public float tamañoArea;
    public GameObject particulas;
    public GameObject particulasExterior;

    private Color colorElemento = new Color32(0xFF, 0xFF, 0xFF, 0xFF);


    [Header("Elemento")]
    public bool fuego = false;
    public bool agua = false;
    public bool roca = false;
    public bool electricidad = false;


    [Header("Muerte manual")]
    public bool muerte = false;
    public int deathSpeed = 40;


    [Header("Materiales elementos")]
    public Material[] materialesElementos;

    [Header("Enemigo grande")]
    public GameObject enemigoGrande;
    private new Renderer renderer;

    [Header("Color particulas")]
    Color rojoPersonalizado;
    Color marronPersonalizado;
    Color azulPersonalizado;
    Color amarilloPersonalizado;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        renderer = enemigoGrande.GetComponent<Renderer>();

        ColorUtility.TryParseHtmlString("#EF4A4A", out rojoPersonalizado);
        ColorUtility.TryParseHtmlString("#B07C66", out marronPersonalizado);
        ColorUtility.TryParseHtmlString("#BDBB6C", out amarilloPersonalizado);
        ColorUtility.TryParseHtmlString("#525FEF", out azulPersonalizado);

        // Generar un número aleatorio entre 0 y 3
        int elementoAleatorio = Random.Range(0, 4);

        // Establecer en true el elemento correspondiente
        switch (elementoAleatorio)
        {
            case 0:
                fuego = true;
                colorElemento = rojoPersonalizado;
                break;

            case 1:
                agua = true;
                colorElemento = azulPersonalizado;
                break;

            case 2:
                roca = true;
                colorElemento = marronPersonalizado;
                break;

            case 3:
                electricidad = true;
                colorElemento = amarilloPersonalizado;
                break;

        }

        // Asignar el material correspondiente
        renderer.material = materialesElementos[elementoAleatorio];

        // Configurar el color de las partículas
        var particleSystem = particulas.GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.startColor = colorElemento;
        particulas.GetComponent<Renderer>().material.color = colorElemento;

        var particleSystemExterior = particulasExterior.GetComponent<ParticleSystem>();
        var mainExterior = particleSystemExterior.main;
        mainExterior.startColor = colorElemento;
        particulasExterior.GetComponent<Renderer>().material.color = colorElemento;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(tamañoArea, transform.localScale.y, tamañoArea);

        // Actualizar el radio del shape de las partículas
        var shapeParticulas = particulas.GetComponent<ParticleSystem>().shape;
        shapeParticulas.radius = tamañoArea / 2;

        var shapeParticulasExterior = particulasExterior.GetComponent<ParticleSystem>().shape;
        shapeParticulasExterior.radius = tamañoArea / 2f;

        if (muerte == true)
        {
            Death();
        }
    }

    public void Death()
    {
        if (tamañoArea > 0)
        {
            tamañoArea -= Time.deltaTime * deathSpeed;
            transform.localScale = new Vector3(tamañoArea, transform.localScale.y, tamañoArea);
        }
        else
        {
            Destroy(enemigoGrande);
        }
    }
}
