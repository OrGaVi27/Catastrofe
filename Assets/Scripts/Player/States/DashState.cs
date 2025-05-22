public class DashState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.baseStats.isInvencible = true;
        player.anim.SetInteger("State", 2);
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
