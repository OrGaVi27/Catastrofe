using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class Corridor : MonoBehaviour
{
    public GameObject originRoom;
    public GameObject adjacentRoom;

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Evento para llamar al fade para cambiar de habitación
            GameEvents.current.RoomFadeTrigger();

            // Activar la habitación adyacente
            adjacentRoom.SetActive(true);
            player.GetComponent<SpawnPointsPlayer>().habitacionActual = adjacentRoom;

            // Esperar al siguiente frame antes de desactivar la habitación actual
            if (originRoom.name == "Jardin" || adjacentRoom.name == "Jardin")
            {
                await Task.Yield();
            }

            // Desactivar la habitación actual de forma segura
            originRoom.SetActive(false);
        }
    }
}

