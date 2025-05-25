using UnityEngine;

public class objetosClave : MonoBehaviour
{
    public string nombreObjet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(nombreObjet);
            Destroy(gameObject);
        }
    }
}
