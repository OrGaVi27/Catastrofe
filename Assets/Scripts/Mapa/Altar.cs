using Unity.VisualScripting;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public GameObject player;
    [Space]
    public GameObject habitacion;
    [Space]
    public GameObject altarActive;
    public GameObject altarDesactive;
    [Space]
    public AudioClip healingSound;
    [Space]
    public GuardarPartida guardado;

    public InteractableItem interact;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player.GetComponent<SpawnPointsPlayer>().spawn == transform.position)
        {
            altarActive.SetActive(true);
            altarDesactive.SetActive(false);
        }
        else
        {
            altarActive.SetActive(false);
            altarDesactive.SetActive(true);
        }

        if (interact.playerInteracted)
        {
            AltarInteract();
        }
    }

    public void AltarInteract()
    {
        interact.playerInteracted = false;

        var stats = player.GetComponent<BasePlayerStats>();
        stats.currentHealth = stats.maxHealth;
        stats.currentMana = stats.maxMana;
        stats.currentHeals = stats.maxHeals;

        player.GetComponent<SpawnPointsPlayer>().habitacionSpawn = habitacion;
        player.GetComponent<SpawnPointsPlayer>().spawn = transform.position;
        ControladorSonido.Instance.EjecutarSonido(healingSound);

        guardado.datosGuardado.habitacionNombre = player.GetComponent<SpawnPointsPlayer>().habitacionSpawn.name;
        guardado.datosGuardado.spawnPosition = transform.position;
        guardado.GuardarJSON();
    }
}