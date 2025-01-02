using UnityEngine;

public class PlayerAttackState : PlayerStateBase
{
    public PlayerAttackState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void EnterState()
    {
        Animator.SetBool("Attacking", true);
    }

    public override void ExitState()
    {
        Animator.SetBool("Attacking", false);
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override PlayerState? GetNextState()
    {
        throw new System.NotImplementedException();
    }
}