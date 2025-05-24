using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance;

    public int currentArea;

    public string[] Areas;
    public AudioClip[] AreaMusic;

    [Space]
    public Canvas AreaUI;
    public TMP_Text AreaNameText;
    public Animator anim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadArea(int areaIndex)
    {
        AreaNameText.text = Areas[areaIndex];
        currentArea = areaIndex;

        //Cambiar la musica del area
        //Función(AreaMusic[currentArea]);

        //Activar texto area
        anim.SetTrigger("Change");
    }
}
