using UnityEngine;

public class PlayerWalkingState : PlayerStateBase
{
    public PlayerWalkingState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void EnterState()
    {
        Animator.SetBool("IsWalk", true);
        InputManager.Instance.isPlayerMoving = true;
    }

    public override void ExitState()
    {
        Animator.SetBool("IsWalk", false);
        InputManager.Instance.isPlayerMoving = false;
    }

    public override void UpdateState()
    {
        
    }

    public override PlayerState? GetNextState()
    {
        throw new System.NotImplementedException();
    }
}