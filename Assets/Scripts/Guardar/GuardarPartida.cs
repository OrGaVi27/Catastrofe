using System.IO;
using UnityEngine;

public class GuardarPartida : MonoBehaviour
{
    public DatosGuardado datosGuardado;
    public string dataPath;
    public string habitacion;
    public string habitacionActual;

    void Awake()
    {
        string carpeta = Application.dataPath + "/CarpetaGuardados/";
        dataPath = carpeta + "PuntosGuardados.json";

        if (!Directory.Exists(carpeta))
        {
            Directory.CreateDirectory(carpeta);
            File.WriteAllText(dataPath, JsonUtility.ToJson(datosGuardado, true));
            Debug.Log("Carpeta creada en: " + carpeta);
            datosGuardado.spawnPosition = new Vector3(-252f, 1.61f, 0f);
        }

        CargarJSON();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) GuardarJSON();
        if (Input.GetKeyDown(KeyCode.L)) CargarJSON();
    }

    public void GuardarJSON()
    {
        /* datosGuardado.habitacionNombre = habitacion;
        datosGuardado.habitacionActualNombre = habitacionActual; */

        string datos = JsonUtility.ToJson(datosGuardado, true);
        File.WriteAllText(dataPath, datos);
        Debug.Log("Guardado correctamente.");
    }
    /*
    public void CargarJSON()
    {
        if (File.Exists(dataPath))
        {
            string datos = File.ReadAllText(dataPath);
            datosGuardado = JsonUtility.FromJson<DatosGuardado>(datos);
            Debug.Log("Datos cargados correctamente.");
        }
        else Debug.LogWarning("Archivo de guardado no encontrado.");
    }
    */
    public void CargarJSON()
{
    if (File.Exists(dataPath))
    {
        string datos = File.ReadAllText(dataPath);
        datosGuardado = JsonUtility.FromJson<DatosGuardado>(datos);

        if (datosGuardado.paginasDiario == null || datosGuardado.paginasDiario.Length != 5)
        {
            datosGuardado.paginasDiario = new bool[5];
            Debug.LogWarning("Se inicializó 'paginasDiario' porque estaba nulo o con tamaño incorrecto.");
        }

        Debug.Log("Datos cargados correctamente.");
    }
    else
    {
        Debug.LogWarning("Archivo de guardado no encontrado.");
    }
}

}
[System.Serializable]
public class DatosGuardado
{
    public bool tutorial = true;
    [Space]
    public Vector3 spawnPosition;
    public string habitacionNombre;
    public string habitacionActualNombre;
    [Space]
    public float musicVolume = 0.5f;
    public float SFXVolume = 0.5f;
    [Space]
    public bool espada01 = false;
    public bool espada02 = false;
    public bool espada03 = false;
    [Space]
    public bool fire = false;
    public bool water = false;
    public bool electricity = false;
    public bool rock = false;
    //paginas del diario, no se como lo tiene adri
    [Space]
    public bool[] paginasDiario = new bool [5];
}