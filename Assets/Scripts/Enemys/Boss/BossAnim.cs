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

    public void sMeleeAttack1()
    {
        boss.sMeleeAttack1();
    }
    public void sMeleeAttack2()
    {
        boss.sMeleeAttack2();
    }
    public void sDistanceAttack1()
    {
        boss.sDistanceAttack1();
    }
    public void sDistanceAttack2()
    {
        boss.sDistanceAttack2();
    }
}
