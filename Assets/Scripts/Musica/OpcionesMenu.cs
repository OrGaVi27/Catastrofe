using UnityEngine;
using UnityEngine.UI;

public class OpcionesMenu : MonoBehaviour
{
    [SerializeField] public Slider sliderMusica;
    [SerializeField] public Slider sliderSFX;

    private void Start()
    {
        // Cargar valores guardados al iniciar
        sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 1f);
        sliderSFX.value = PlayerPrefs.GetFloat("VolumenEfectos", 1f);

        // Agregar listeners
        sliderMusica.onValueChanged.AddListener(CambiarVolumenMusica);
        sliderSFX.onValueChanged.AddListener(CambiarVolumenSFX);
    }

    public void CambiarVolumenMusica(float valor)
    {
        if (ControladorMusica.Instance != null)
            ControladorMusica.Instance.CambiarVolumenMusica(valor);
    }

    public void CambiarVolumenSFX(float valor)
{
    if (ControladorSonido.Instance != null)
    {
        ControladorSonido.Instance.CambiarVolumenEfectos(valor);
    }
    else
    {
        PlayerPrefs.SetFloat("VolumenEfectos", valor);
        PlayerPrefs.Save();
    }
}
}
