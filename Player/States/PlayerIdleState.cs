using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(Player entity, Animator animator) : base(entity, animator)
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
        // if (!Entity.PlayerReachedDestination())
        // {
        //     PlayerStateManager.Instance.SetState(PlayerState.Walking);
        // }    
    }

    public override PlayerState? GetNextState()
    {
        throw new System.NotImplementedException();
    }
}