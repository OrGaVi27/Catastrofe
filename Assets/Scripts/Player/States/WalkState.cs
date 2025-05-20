
// This code defines a WalkState class that inherits from PlayerBaseState.
public class WalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player) 
    { 
        
    }
    public override void ExitState(PlayerStateManager player) { }
    public override void UpdateState(PlayerStateManager player)
    {
        
        if (player.dashIsOn)
        {
            player.SwitchState(player.dashState);
        }

        if (player.isAttacking)
        {
            player.SwitchState(player.attackState);
        }

        if (player.inputVector.magnitude == 0)
        {
            player.SwitchState(player.idleState);
        }
        else
        {
            player.Move();
        }
    }
}
