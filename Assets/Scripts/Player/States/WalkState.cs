
// This code defines a WalkState class that inherits from PlayerBaseState.

using UnityEngine;

public class WalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.anim.SetInteger("State", 1);
    }
    public override void ExitState(PlayerStateManager player) { }
    public override void UpdateState(PlayerStateManager player)
    {
        if (player.isAttacking)
        {
            player.SwitchState(player.attackState);
        }
        
        if (player.dashIsOn)
        {
            player.SwitchState(player.dashState);
        }
        else if (player.inputVector.magnitude == 0)
        {
            player.SwitchState(player.idleState);
        }
        else
        {
            player.Move();
        }
    }
}
