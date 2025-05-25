using UnityEngine;

public class ControladorMusica : MonoBehaviour
{
    public static ControladorMusica Instance;
    public AudioSource audioSource;
    public float volumenMusica = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("No se encontró un componente AudioSource en el GameObject de ControladorMusica.");
                return;
            }

            // Configurar AudioSource para música de fondo
            audioSource.loop = true;

            // Cargar volumen guardado
            volumenMusica = PlayerPrefs.GetFloat("VolumenMusica", 1f);
            audioSource.volume = volumenMusica;

            Debug.Log("Volumen música cargado: " + volumenMusica);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CambiarVolumenMusica(float valor)
    {
        volumenMusica = valor;
        audioSource.volume = volumenMusica;
        PlayerPrefs.SetFloat("VolumenMusica", volumenMusica);
        PlayerPrefs.Save();
        Debug.Log("Volumen música cambiado a: " + volumenMusica);
    }

    public void ReproducirMusica(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void DetenerMusica()
    {
        audioSource.Stop();
    }
}
