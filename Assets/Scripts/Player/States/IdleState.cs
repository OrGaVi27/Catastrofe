
public class IdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player) { }
    public override void ExitState(PlayerStateManager player) { }
    public override void UpdateState(PlayerStateManager player)
    {
        if (player.inputVector.magnitude != 0)
        {
            player.SwitchState(player.walkState);
        }

        if (player.dashIsOn)
        {
            player.SwitchState(player.dashState);
        }

        if (player.isAttacking)
        {
            player.SwitchState(player.attackState);
        }
    }
}
