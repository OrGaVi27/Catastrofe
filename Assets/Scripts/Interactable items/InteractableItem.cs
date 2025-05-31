using UnityEngine;

[RequireComponent(typeof(EnemyVisionArea))]
public class InteractableItem : MonoBehaviour
{
    public bool playerInteracted = false;

    void Start()
    {
        GameEvents.current.onInteract += OnInteract;
    }

    private void OnInteract()
    {
        if (GetComponent<EnemyVisionArea>().playerInRange)
        {
            playerInteracted = true;
            //GetComponent<Renderer>().material.color = Color.green; // Change color to indicate interaction
        }
    }
}
