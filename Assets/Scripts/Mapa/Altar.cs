using Unity.VisualScripting;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public GameObject player;
    [Space]
    public GameObject habitacion;
    [Space]
    public GameObject altar;
    public GameObject altarActive;
    public GameObject altarDesactive;
    [Space]
    public AudioClip healingSound;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player.GetComponent<SpawnPointsPlayer>().spawn.transform.position == gameObject.transform.position)
        {
            altarActive.SetActive(true);
            altarDesactive.SetActive(false);
        }
        else
        {
            altarActive.SetActive(false);
            altarDesactive.SetActive(true);
        }
    }

    public void AltarInteract()
    {
        player.GetComponent<BasePlayerStats>().currentHealth = player.GetComponent<BasePlayerStats>().maxHealth;

        player.GetComponent<BasePlayerStats>().currentMana = player.GetComponent<BasePlayerStats>().maxMana;

        player.GetComponent<BasePlayerStats>().currentHeals = player.GetComponent<BasePlayerStats>().maxHeals;

        player.GetComponent<SpawnPointsPlayer>().habitacionSpawn = habitacion;

        player.GetComponent<SpawnPointsPlayer>().spawn.transform.position = gameObject.transform.position;

        ControladorSonido.Instance.EjecutarSonido(healingSound);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AltarInteract();
        }
    }
}