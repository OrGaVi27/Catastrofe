using UnityEngine;
using UnityEngine.UI;

public class VolumenEfectosControl : MonoBehaviour
{
    public Slider slider;
    public Image imageMute;

    private float sliderValue;

    void Start()
    {
        sliderValue = PlayerPrefs.GetFloat("VolumenEfectos", 1f);
        Debug.Log("Slider carga valor: " + sliderValue);
        slider.value = sliderValue;

        if (ControladorSonido.Instance != null)
            ControladorSonido.Instance.CambiarVolumenEfectos(sliderValue);

        RevisarMute();
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;

        if (ControladorSonido.Instance != null)
            ControladorSonido.Instance.CambiarVolumenEfectos(sliderValue);

        RevisarMute();
    }

    void RevisarMute()
    {
        imageMute.enabled = (sliderValue == 0);
    }
}
