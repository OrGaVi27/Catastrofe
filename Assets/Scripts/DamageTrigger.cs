using Unity.VisualScripting;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public GameObject attaker;
    public float damageAmount = 0f;
    public int element = 0;
    void Start()
    {
    }

    void Update()
    {
        element = attaker.GetComponent<PlayerStateManager>().element;
    }

    void OnTriggerEnter(Collider other)
    {
        /*  if (other.CompareTag(targetTag))
         {
             Debug.Log("Damage Triggered: " + other.gameObject.name);
             other.gameObject.GetComponent<MageBehaviour>().TakeDamage(damageAmount);
         } */
        if (other.gameObject.GetComponent<BaseEnemyStats>() != null)
        {
            other.gameObject.GetComponent<BaseEnemyStats>().TakeDamage(attaker.GetComponent<PlayerStateManager>().DamageOutput(), element);
            
            if (attaker.GetComponent<BasePlayerStats>().currentMana < attaker.GetComponent<BasePlayerStats>().maxMana && element == 0)
            {
                attaker.GetComponent<BasePlayerStats>().currentMana++;
            }
        }
    }

}
