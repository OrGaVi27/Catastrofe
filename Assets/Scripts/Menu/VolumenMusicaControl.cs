using UnityEngine;
using UnityEngine.UI;

public class VolumenMusicaControl : MonoBehaviour
{
    public Slider slider;
    public Image imageMute;
    public AudioSource musicaMenuAudioSource;

    private float sliderValue;

    void Start()
    {
        sliderValue = PlayerPrefs.GetFloat("volumenMusica", 0.5f);
        slider.value = sliderValue;

        if (musicaMenuAudioSource != null)
            musicaMenuAudioSource.volume = sliderValue;

        RevisarMute();
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenMusica", sliderValue);
        PlayerPrefs.Save();  // Guarda cambios inmediatamente

        if (musicaMenuAudioSource != null)
            musicaMenuAudioSource.volume = sliderValue;

        RevisarMute();
    }

    void RevisarMute()
    {
        imageMute.enabled = (sliderValue == 0);
    }
}