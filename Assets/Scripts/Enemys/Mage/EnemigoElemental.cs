using UnityEngine;
using UnityEngine.UI;

public class EnemigoElemental : MonoBehaviour
{
    [Header("Area")]
    public float tamañoArea;
    public GameObject particulas;
    public GameObject particulasExterior;
    private Color colorElemento = new Color32(0xFF, 0xFF, 0xFF, 0xFF);


    [Header("Elemento")]
    public int elementoAleatorio;
    public int numeroElemento;
    public bool fuego = false;
    public bool agua = false;
    public bool roca = false;
    public bool electricidad = false;


    [Header("Vida")]
    public Image Vida;
    public Image Vida2;

    public Material[] materialesElementos;
    public Material[] materialesOjos;

    public GameObject[] ojosMago;
    public GameObject[] partesMago;

    public Sprite[] iconosElementos;
    public Image iconoElemento;

    // [Header("Materiales elementos")]
    // public Material[] materialesElementos;

    [Header("Enemigo grande")]
    private new Renderer renderer;

    [Header("Color particulas")]
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

        // Generar un número aleatorio entre 0 y 3
        elementoAleatorio = Random.Range(0, 4);
        numeroElemento = elementoAleatorio;
    }

    void OnEnable()
    {
        elementoAleatorio = Random.Range(0, 4);
        numeroElemento = elementoAleatorio;
    }

    // Update is called once per frame
    void Update()
    {
        // Establecer en true el elemento correspondiente
        switch (elementoAleatorio)
        {
            case 0:
                fuego = true;
                foreach (GameObject parte in partesMago)
                {
                    parte.GetComponent<Renderer>().material = materialesElementos[0];
                }
                ojosMago[0].GetComponent<Renderer>().material = materialesOjos[0];
                ojosMago[1].GetComponent<Renderer>().material = materialesOjos[0];

                colorElemento = rojoPersonalizado;
                Vida.color = rojoPersonalizado;
                Vida2.color = rojoPersonalizado;
                particulas.SetActive(true);
                particulasExterior.SetActive(true);
                iconoElemento.sprite = iconosElementos[0];
                iconoElemento.color = Color.white;

                break;

            case 3:
                agua = true;
                foreach (GameObject parte in partesMago)
                {
                    parte.GetComponent<Renderer>().material = materialesElementos[3];
                }
                ojosMago[0].GetComponent<Renderer>().material = materialesOjos[3];
                ojosMago[1].GetComponent<Renderer>().material = materialesOjos[3];
                colorElemento = azulPersonalizado;
                Vida.color = azulPersonalizado;
                Vida2.color = azulPersonalizado;
                particulas.SetActive(true);
                particulasExterior.SetActive(true);
                iconoElemento.sprite = iconosElementos[3];
                iconoElemento.color = Color.white;

                break;

            case 1:
                roca = true;
                foreach (GameObject parte in partesMago)
                {
                    parte.GetComponent<Renderer>().material = materialesElementos[1];
                }
                ojosMago[0].GetComponent<Renderer>().material = materialesOjos[1];
                ojosMago[1].GetComponent<Renderer>().material = materialesOjos[1];

                colorElemento = marronPersonalizado;
                Vida.color = marronPersonalizado;
                Vida2.color = marronPersonalizado;
                particulas.SetActive(true);
                particulasExterior.SetActive(true);
                iconoElemento.sprite = iconosElementos[1];
                iconoElemento.color = Color.white;

                break;

            case 2:
                electricidad = true;
                foreach (GameObject parte in partesMago)
                {
                    parte.GetComponent<Renderer>().material = materialesElementos[2];
                }
                ojosMago[0].GetComponent<Renderer>().material = materialesOjos[2];
                ojosMago[1].GetComponent<Renderer>().material = materialesOjos[2];

                colorElemento = amarilloPersonalizado;
                Vida.color = amarilloPersonalizado;
                Vida2.color = amarilloPersonalizado;
                particulas.SetActive(true);
                particulasExterior.SetActive(true);
                iconoElemento.sprite = iconosElementos[2];
                iconoElemento.color = Color.white;
                break;
            case 4:
                fuego = false;
                agua = false;
                roca = false;
                electricidad = false;

                foreach (GameObject parte in partesMago)
                {
                    parte.GetComponent<Renderer>().material = materialesElementos[4];
                }
                ojosMago[0].GetComponent<Renderer>().material = materialesOjos[4];
                ojosMago[1].GetComponent<Renderer>().material = materialesOjos[4];

                colorElemento = Color.white;
                Vida.color = Color.white;

                particulas.SetActive(false);
                particulasExterior.SetActive(false);
                iconoElemento.sprite = null;
                iconoElemento.color = Color.clear;

                break;

        }

        // Asignar el material correspondiente
        // renderer.material = materialesElementos[elementoAleatorio];

        // Configurar el color de las partículas
        var particleSystem = particulas.GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.startColor = colorElemento;
        particulas.GetComponent<Renderer>().material.color = colorElemento;
        // Debug.Log("Color de las partículas: " + colorElemento);

        var particleSystemExterior = particulasExterior.GetComponent<ParticleSystem>();
        var mainExterior = particleSystemExterior.main;
        mainExterior.startColor = colorElemento;
        particulasExterior.GetComponent<Renderer>().material.color = colorElemento;
        // Debug.Log("Color de las partículas exterior: " + colorElemento);


        transform.localScale = new Vector3(tamañoArea, transform.localScale.y, tamañoArea);

        // Actualizar el radio del shape de las partículas
        var shapeParticulas = particulas.GetComponent<ParticleSystem>().shape;
        shapeParticulas.radius = tamañoArea / 2;

        var shapeParticulasExterior = particulasExterior.GetComponent<ParticleSystem>().shape;
        shapeParticulasExterior.radius = tamañoArea / 2f;

    }
}
