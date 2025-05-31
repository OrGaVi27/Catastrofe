using UnityEngine;

public class BasePlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public int maxHeals = 3;
    public int currentHeals;
    [SerializeField] private AudioClip Hit;

    public int maxMana = 5;
    public int currentMana;
    public float damage = 0f;
    public float DamageMultiplier = 1f;
    public float elementMultiplier = 0;
    public bool isInvencible = false;

    public void TakeDamage(float damageAmount)
    {
        if (!isInvencible)
        {
            GetComponent<PlayerStateManager>().anim.SetTrigger("Hit");
            CameraShakeManager.instance.ShakeCamera(GetComponent<PlayerStateManager>().GetComponent<Cinemachine.CinemachineImpulseSource>(), 1f);
            ControladorSonido.Instance.EjecutarSonido(Hit);
            currentHealth -= damageAmount;

            if (currentHealth <= 0f)
            {
                Die();
            }
        }

    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        Debug.Log("Player has died.");
    }
}
