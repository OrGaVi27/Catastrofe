using UnityEngine;

public class DamageTriggerMinion : MonoBehaviour
{
    public GameObject player;
    public float damage;
    protected int hits = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void OnEnable()
    {
        hits = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hits == 0)
        {
            hits++;
            player.GetComponent<BasePlayerStats>().TakeDamage(damage); // Llama a la función de daño del jugador
        }

    }
}
