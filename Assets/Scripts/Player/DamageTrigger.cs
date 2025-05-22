using Unity.VisualScripting;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public GameObject attaker;
    public float damageAmount = 0f;
    public int element = 0;
    public float elementMultiplier = 1;


    void Start()
    {

    }


    void Update()
    {
        element = attaker.GetComponent<PlayerStateManager>().element;
        elementMultiplier = attaker.GetComponent<BasePlayerStats>().elementMultiplier;
        damageAmount = attaker.GetComponent<PlayerStateManager>().DamageOutput();
    }


    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<BaseEnemyStats>() != null)
        {
            other.gameObject.GetComponent<BaseEnemyStats>().TakeDamage(damageAmount, element, elementMultiplier);

            
            
            if (attaker.GetComponent<BasePlayerStats>().currentMana < attaker.GetComponent<BasePlayerStats>().maxMana && element == 0)
            {
                attaker.GetComponent<BasePlayerStats>().currentMana++;
            }
        }
    }

}
