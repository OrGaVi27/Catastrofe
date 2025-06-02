using Unity.VisualScripting;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public GameObject player;

    public GameObject habitacion;

    public GameObject altar;
    public Material[] materials;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void AltarInteract()
    {
        altar.GetComponent<Renderer>().material = materials[1];

        player.GetComponent<BasePlayerStats>().currentHealth = player.GetComponent<BasePlayerStats>().maxHealth;

        player.GetComponent<BasePlayerStats>().currentMana = player.GetComponent<BasePlayerStats>().maxMana;

        //Pociones al maxisimo
        Debug.Log("potis max");

        player.GetComponent<SpawnPointsPlayer>().habitacionSpawn = habitacion;

        player.GetComponent<SpawnPointsPlayer>().spawn.transform.position = gameObject.transform.position;

        //Sonido de curar
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            altar.GetComponent<Renderer>().material = materials[1];

            player.GetComponent<BasePlayerStats>().currentHealth = player.GetComponent<BasePlayerStats>().maxHealth;

            player.GetComponent<BasePlayerStats>().currentMana = player.GetComponent<BasePlayerStats>().maxMana;

            //Pociones al maxisimo
            Debug.Log("potis max");

            player.GetComponent<SpawnPointsPlayer>().habitacionSpawn = habitacion;

            player.GetComponent<SpawnPointsPlayer>().spawn.transform.position = gameObject.transform.position;
        }
    }

    void OnTriggerExit()
    {
        altar.GetComponent<Renderer>().material = materials[0];
    }
}