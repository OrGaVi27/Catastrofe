using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : MonoBehaviour
{
    public BossBehaviour boss;
    public DamagePlayer damage;

    public void MeleeIdle()
    {
        boss.MeleeIdle();
    }

    public void DsitanceIdle()
    {
        boss.DsitanceIdle();
    }

    public void ResetTimeAttack()
    {
        boss.ResetTimeAttack();
    }

    public void DestoryBoss()
    {
        boss.DestoryBoss();
    }

    public void InstanceBolas()
    {
        boss.InstanceBolas();
    }

    public void InstanceRayo()
    {
        boss.InstanceRayo();
    }

    public void Damage()
    {
        damage.ActiveDamage();
    }
}
