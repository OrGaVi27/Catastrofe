using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonido : MonoBehaviour
{
    public static ControladorSonido Instance;
    public AudioSource audioSource;
    public float volumenEfectos = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();

            // Carga volumen guardado al inicio
            volumenEfectos = PlayerPrefs.GetFloat("VolumenEfectos", 1f);
            audioSource.volume = volumenEfectos;
            Debug.Log("Volumen cargado en Awake: " + volumenEfectos);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void CambiarVolumenEfectos(float valor)
    {
        volumenEfectos = valor;
        audioSource.volume = volumenEfectos;
        PlayerPrefs.SetFloat("VolumenEfectos", volumenEfectos);
        PlayerPrefs.Save();  // Guarda inmediatamente
        Debug.Log("Volumen cambiado a: " + volumenEfectos);
    }

    public void ActualizarVolumen()
    {
        volumenEfectos = PlayerPrefs.GetFloat("VolumenEfectos", 1f);
        audioSource.volume = volumenEfectos;
    }

    public void EjecutarSonido(AudioClip sonido)
    {
        audioSource.PlayOneShot(sonido);
    }
}
