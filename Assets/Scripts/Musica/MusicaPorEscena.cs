using UnityEngine;

public class MusicaPorEscena : MonoBehaviour
{
    public AudioClip musicaDeEstaEscena;

    void Start()
    {
        if (ControladorMusica.Instance != null && musicaDeEstaEscena != null)
        {
            ControladorMusica.Instance.ReproducirMusica(musicaDeEstaEscena);
        }
    }
}
