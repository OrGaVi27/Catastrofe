using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : MonoBehaviour
{
    public BossBehaviour boss;

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

    public void InstancePreRayo()
    {
        boss.InstancePreRayo();
    }

    public void InstanceRayo()
    {
        boss.InstanceRayo();
    }

    public void MeleeSwordDamage1()
    {
        boss.MeleeSwordDamage1();
    }

    public void MeleeSwordDamage2()
    {
        boss.MeleeSwordDamage2();
    }
}
