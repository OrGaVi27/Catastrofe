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
}
