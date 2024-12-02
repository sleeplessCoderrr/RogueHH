using UnityEngine;

public class PlayerWalkingState : PlayerStateBase
{
    public PlayerWalkingState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entering Walking State");
        Animator.SetBool("IsWalk", true);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Walking State");
        Animator.SetBool("IsWalk", false);
    }

    public override void UpdateState()
    {
        
    }

    public override PlayerState? GetNextState()
    {
        throw new System.NotImplementedException();
    }
}