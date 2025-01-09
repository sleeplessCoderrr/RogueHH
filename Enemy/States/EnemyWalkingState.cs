using UnityEngine;

public class EnemyWalkingState : EnemyBaseState
{
    public EnemyWalkingState(Enemy enemy, Animator animator) : base(enemy, animator)
    {
    }

    public override void EnterState()
    {
        Animator.SetBool("IsWalk", true);
    }

    public override void ExitState()
    {
        Animator.SetBool("IsWalk", false);
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