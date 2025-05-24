using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class Corridor : MonoBehaviour
{
    public GameObject originRoom;
    public GameObject adjacentRoom;
    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Evento para llamar al fade para cambiar de habitación
            GameEvents.current.RoomFadeTrigger();

            // Activar la habitación adyacente
            adjacentRoom.SetActive(true);

            // Esperar al siguiente frame antes de desactivar la habitación actual
            await Task.Yield();

            // Desactivar la habitación actual de forma segura
            originRoom.SetActive(false);
        }
    }
}

