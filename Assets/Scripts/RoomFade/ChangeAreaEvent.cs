using Unity.VisualScripting;
using UnityEngine;

public class ChangeAreaEvent : MonoBehaviour
{
    [Tooltip("Index del area a la que vas a apasar.")]
    public int areaIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) AreaManager.instance.LoadArea(areaIndex);
    }
}
