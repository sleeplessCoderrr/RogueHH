using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Enemy enemy, Animator animator) : base(enemy, animator)
    {
    }

    public override void EnterState()
    {
        Animator.SetBool("IsIdle", true);
    }

    public override void ExitState()
    {
        Animator.SetBool("IsIdle", false);
    }

    public override void UpdateState()
    { return; }

    public override PlayerState? GetNextState()
    {
        return null;
    }
}
