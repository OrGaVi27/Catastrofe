
public class DashState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player) { }
    public override void ExitState(PlayerStateManager player) { }
    public override void UpdateState(PlayerStateManager player)
    {
        player.Dash();

        if (!player.dashIsOn)
        {
            player.SwitchState(player.idleState);
        }
    }
}
