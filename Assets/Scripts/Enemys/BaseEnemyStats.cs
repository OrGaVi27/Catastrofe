using UnityEngine;
using UnityEngine.AI;


public class BaseEnemyStats : MonoBehaviour
{
    [Header("Stats")]

    public float maxHealth = 100f;
    public float currentHealth;
    public float damage = 0f;
    public float movementSpeed = 1f;
    public int element;
    public bool dead = false;
    [Header("Sonidos")]
    [SerializeField] private AudioClip Hit;
    [SerializeField] private AudioClip Dead;


    [Header("Animaciones")]
    public Animator animator;

    [Header("NavMesh")]
    public NavMeshAgent nmAgent;

    [Header("Canvas")]
    public Canvas canvas;


    public void TakeDamage(float damageAmount, int attackElement, float elementMultiplier)
    {


        if (attackElement == 0)
        {
            currentHealth -= damageAmount;
            animator.SetTrigger("Hit");
        }
        else if (element == 0)
        {
            currentHealth -= damageAmount;
            animator.SetTrigger("Hit");
        }
        else if (attackElement == 1 && element == 4)
        {
            currentHealth -= damageAmount * 0.5f;
            animator.SetTrigger("Hit");
        }
        else if (attackElement == 4 && element == 1)
        {
            currentHealth -= damageAmount * elementMultiplier;
            animator.SetTrigger("BigHit");
        }
        else if (attackElement - 1 == element)
        {
            currentHealth -= damageAmount * 0.5f;
            animator.SetTrigger("Hit");
        }
        else if (attackElement + 1 == element)
        {
            currentHealth -= damageAmount * elementMultiplier;
            animator.SetTrigger("BigHit");
        }
        else
        {
            currentHealth -= damageAmount;
            animator.SetTrigger("Hit");
        }


        if (currentHealth <= 0f)
        {
            dead = true;
            Die();
        }
        else
        {
            //ControladorSonido.Instance.EjecutarSonido(Hit);
        }


    }

    public void Heal(float healAmount)
    {
        if (dead) return;
        animator.SetTrigger("Heal");
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        //Debug.Log("Muerto");

        animator.SetBool("Death", true);
        //ControladorSonido.Instance.EjecutarSonido(Dead);

        dead = true;

        if (nmAgent != null)
        {
            nmAgent.enabled = false;
        }

        // Opcional: Desactivar colisiones si es necesario
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        canvas.gameObject.SetActive(false); // Desactiva el canvas del enemigo

        Invoke("DestroyObject", 10f); // Llama a Desaparecer después de 15 segundos
    }

    public void DestroyObject()
    {
        //Debug.Log("Desaparecer");
        Destroy(gameObject);
    }
}
