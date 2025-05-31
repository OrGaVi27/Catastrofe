using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public GameObject attaker;
    public float damageAmount = 0f;
    public int element = 0;
    public float elementMultiplier = 1;
    int hits = 0;


    void Start()
    {
        attaker = GameObject.FindGameObjectWithTag("Player");
    }


    void OnEnable()
    {
        element = attaker.GetComponent<PlayerStateManager>().element;
        elementMultiplier = attaker.GetComponent<BasePlayerStats>().elementMultiplier;
        damageAmount = attaker.GetComponent<PlayerStateManager>().DamageOutput();

        hits = 0;
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

        if (other.gameObject.GetComponent<BossBehaviour>() != null)
        {
            other.gameObject.GetComponent<BossBehaviour>().TakeDamage(damageAmount, element, elementMultiplier);

            if (attaker.GetComponent<BasePlayerStats>().currentMana < attaker.GetComponent<BasePlayerStats>().maxMana && element == 0)
            {
                attaker.GetComponent<BasePlayerStats>().currentMana++;
            }
        }

        if (other.GetComponent<PuzleActivator>() != null)
        {
            if (hits == 0)
            {
                hits++;
                PuzleActivator pa = other.GetComponent<PuzleActivator>();
                pa.wasActivated = true;
                pa.SetAttackElement(element);

                if (attaker.GetComponent<BasePlayerStats>().currentMana < attaker.GetComponent<BasePlayerStats>().maxMana && element == 0)
                {
                    attaker.GetComponent<BasePlayerStats>().currentMana++;
                }
            }
        }

    }

    public void SetDamage(float damageAmount, int element, float elementMultiplier)
    {
        this.damageAmount = damageAmount;
        this.element = element;
        this.elementMultiplier = elementMultiplier;
    }

}
