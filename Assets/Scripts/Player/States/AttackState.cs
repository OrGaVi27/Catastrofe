
using UnityEngine;

public class AttackState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.anim.SetBool("IsAttacking", true);
        if (player.element != 0 && player.baseStats.currentMana > 0) player.baseStats.currentMana--;

    }
    public override void ExitState(PlayerStateManager player)
    {
        player.anim.SetBool("IsAttacking", false);
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.isAttacking && player.inputVector.magnitude != 0)
        {
            player.SwitchState(player.walkState);
        }
        else if (!player.isAttacking)
        {
            player.SwitchState(player.idleState);
        }


        if (player.attackIsCharging)
        {
            player.anim.SetBool("IsCharging", true);
            if (player.rangeCharacter)
            {
                player.aimAid.SetActive(true);
                player.virtualCamera.Priority = 8;
            }

            if (player.attackChargeTime + player.attackStartTime > Time.time)
            {
                //Debug.Log("normal attack");
                player.anim.SetBool("StrongAttack", false);
            }
            else
            {
                //Debug.Log("STRONG ATTACK!");
                player.anim.SetBool("StrongAttack", true);
            }
        }
        else
        {
            player.anim.SetBool("IsCharging", false);
            if (player.rangeCharacter)
            {
                player.aimAid.SetActive(false);
                player.virtualCamera.Priority = 10;
            }
            player.Attack();
        }

        player.Move(player.attackMovSpeed);
    }
}
