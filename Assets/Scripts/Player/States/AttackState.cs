
using UnityEngine;

public class AttackState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {

    }
    public override void ExitState(PlayerStateManager player)
    {
        
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
