using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public string targetTag;
    public GameObject attaker;
    public float damageAmount = 0f;
    void Start()
    {
        damageAmount = attaker.GetComponent<BasePlayerStats>().damage;
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
            other.gameObject.GetComponent<BaseEnemyStats>().TakeDamage(damageAmount);
        }
    }

}
