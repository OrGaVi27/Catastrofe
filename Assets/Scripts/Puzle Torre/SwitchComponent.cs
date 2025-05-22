using UnityEngine;

public class SwitchComponent : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameEvents.current.SwitchTrigger(id);
        }
    }
}
