public class FallState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player) { }
    public override void ExitState(PlayerStateManager player) { }
    public override void UpdateState(PlayerStateManager player)
    {
        if (player.controller.isGrounded)
        {
            player.SwitchState(player.idleState);
        }
        else
        {
            player.Move(4);
        }
    }
}
