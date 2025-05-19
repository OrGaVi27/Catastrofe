using UnityEngine;

public class MageHeal : MonoBehaviour
{
    public MageBehaviour mage;

    public void Heal()
    {
        mage.HealTeam(mage.healAmount);
    }
}
