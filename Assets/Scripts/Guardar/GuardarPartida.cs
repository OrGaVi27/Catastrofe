using System.IO;
using UnityEngine;

public class GuardarPartida : MonoBehaviour
{
    public DatosGuardado datosGuardado = new DatosGuardado(); 
    string dataPath;

    public void Awake()
    {
        string carpeta = Application.dataPath + "/CarpetaGuardados/";
        if (!Directory.Exists(carpeta))
        {
            // Crear la carpeta si no existe
            Directory.CreateDirectory(carpeta); 
            
            Debug.Log("Carpeta creada en: " + carpeta);
        }
        // Ruta completa del archivo
        dataPath = carpeta + "DatosGuardados.json"; 
        // Intentar cargar al inicio 
        CargarJSON();
    }
    
    public void Update()
    {
        //improvisado para debug
        /* if (Input.GetKeyDown("p"))
        {
            GuardarJSON();
        }
        if (Input.GetKeyDown("l"))
        {
            CargarJSON();
        } */
    }

    public void GuardarJSON()
    {
        string datos = JsonUtility.ToJson(datosGuardado);
        System.IO.File.WriteAllText(dataPath, datos);

        Debug.Log("Guardado");
    }

    public void CargarJSON()
    {
        string datos =  System.IO.File.ReadAllText(dataPath);
        datosGuardado = JsonUtility.FromJson<DatosGuardado>(datos);

        Debug.Log("Cargado");
    }

}
[System.Serializable]
public class DatosGuardado
{
    public bool tutorial = true;
    [Space]
    public Vector3 spawnPosition;
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
}
