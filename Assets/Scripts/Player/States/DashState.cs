
public class DashState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.baseStats.isInvencible = true;
    }
    public override void ExitState(PlayerStateManager player)
    {
        player.baseStats.isInvencible = false;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        player.Dash();

        if (!player.dashIsOn)
        {
            player.SwitchState(player.idleState);
        }
    }
}
