using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void EnterState()
    {
        Animator.SetBool("IsWalk", false);
    }

    public override void ExitState()
    {
        Animator.SetBool("IsWalk", true);
    }

    public override void UpdateState()
    {
      
    }

    public override PlayerState? GetNextState()
    {
        throw new System.NotImplementedException();
    }
}