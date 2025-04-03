using UnityEngine;

public class Corridor : MonoBehaviour
{
    public GameObject originRoom;
    public GameObject adjacentRoom;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Evento para llamar al fade Para cambiar de habitación
            GameEvents.current.RoomFadeTrigger();
        
            //Activar la habitación adyacente y desactivar la actual
            adjacentRoom.SetActive(true);
            originRoom.SetActive(false);
        }
    }
}
