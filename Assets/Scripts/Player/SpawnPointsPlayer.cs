using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPointsPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 spawn;
    public GameObject habitacionSpawn;
    public GameObject habitacionActual;
    public GameObject guardado;

    public GameObject canvasMuerte;

    void Start()
    {
        GuardarPartida gp = guardado.GetComponent<GuardarPartida>();
        transform.position = gp.datosGuardado.spawnPosition;

        if (gp.datosGuardado.habitacionNombre == "")
        {
            gp.datosGuardado.habitacionNombre = habitacionSpawn.name;

            HabitacionesController.instance.Cargado(habitacionSpawn.name);
            spawn = gp.datosGuardado.spawnPosition;
        }
        else
        {
            HabitacionesController.instance.Cargado(gp.datosGuardado.habitacionNombre);

            habitacionSpawn = GameObject.Find(gp.datosGuardado.habitacionNombre);

            spawn = gp.datosGuardado.spawnPosition;
        }
    }

    void Update()
    {
        if (player.GetComponent<BasePlayerStats>().currentHealth <= 0f)
        {
            canvasMuerte.SetActive(true);
            player.GetComponent<PlayerStateManager>().anim.SetBool("Death", true);

        }
    }

    public void Spawn()
    {

        if (habitacionActual != habitacionSpawn)
        {
            habitacionActual.SetActive(false);
            habitacionSpawn.SetActive(true);
        }

        transform.position = spawn;
        Physics.SyncTransforms();

        var stats = player.GetComponent<BasePlayerStats>();
        stats.currentHealth = stats.maxHealth;
        stats.currentMana = stats.maxMana;
        stats.currentHeals = stats.maxHeals;


        var gp = guardado.GetComponent<GuardarPartida>();
        if (habitacionSpawn != null)
        {
            gp.datosGuardado.habitacionNombre = habitacionSpawn.name;
        }
        if (habitacionActual != null)
        {
            gp.datosGuardado.habitacionActualNombre = habitacionActual.name;
        }
        canvasMuerte.SetActive(false);

    }
}
