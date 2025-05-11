
public class AttackState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.anim.SetBool("IsAttacking", true);
    }
    public override void ExitState(PlayerStateManager player) 
    {
        player.anim.SetBool("IsAttacking", false);
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (player.attackIsCharging)
        {
            player.anim.SetBool("IsCharging", true);
            player.anim.SetLayerWeight(1, 1);
        }else
        {
            player.anim.SetBool("IsCharging", false);
            player.Attack();
        }
        
        
        
        if (!player.isAttacking)
        {
            player.SwitchState(player.idleState);
        }

        player.Move(player.attackMovSpeed);
    }
}
