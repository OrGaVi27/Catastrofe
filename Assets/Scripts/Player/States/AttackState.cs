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
            float timeCharging = Time.time - player.attackStartTime;
            
            if (timeCharging >= 0.5f && !player.chargingSoundPlayed)
            {
                ControladorSonido.Instance.EjecutarSonido(player.ChargingAttack);
                player.chargingSoundPlayed = true;
            }

            if (player.rangeCharacter)
            {
                player.aimAid.SetActive(true);
                player.virtualCamera.Priority = 8;
            }

            if (timeCharging < player.attackChargeTime)
            {
                player.anim.SetBool("StrongAttack", false);
            }
            else
            {
                player.anim.SetBool("StrongAttack", true);
            }
        }
        else
        {
            player.chargingSoundPlayed = false;

            if (player.rangeCharacter)
            {
                player.aimAid.SetActive(false);
                player.virtualCamera.Priority = 10;
            }

            if (player.dashIsOn)
            {
                player.isAttacking = false;
                player.SwitchState(player.dashState);
            }

            player.Attack();
        }

        player.Move(player.attackMovSpeed);
    }
}
