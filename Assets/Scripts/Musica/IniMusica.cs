using UnityEngine;

public class IniMusica : MonoBehaviour
{
    public GameObject prefabControladorMusica;

    private void Awake()
    {
        if (ControladorMusica.Instance == null)
        {
            Instantiate(prefabControladorMusica);
        }
    }
}
