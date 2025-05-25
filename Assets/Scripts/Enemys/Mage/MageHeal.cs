using UnityEngine;

public class MageHeal : MonoBehaviour
{
    public MageBehaviour mage;
    [SerializeField] public AudioClip healingSound;

    public void Heal()
    {
        mage.HealTeam(mage.healAmount);
        ControladorSonido.Instance.EjecutarSonido(healingSound);

    }
}
