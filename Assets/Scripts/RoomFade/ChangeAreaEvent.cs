using Unity.VisualScripting;
using UnityEngine;

public class ChangeAreaEvent : MonoBehaviour
{
    [Tooltip("Index del area a la que vas a apasar.")]
    public int areaIndex;

    public void OnDisable()
    {
        AreaManager.instance.LoadArea(areaIndex);
    }
}
