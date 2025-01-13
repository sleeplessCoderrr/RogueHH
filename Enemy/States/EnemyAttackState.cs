using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(Enemy enemy, Animator animator) : base(enemy, animator)
    {
    }

    public override void EnterState()
    {
        Animator.SetBool("IsAttacking", true);
    }

    public override void ExitState()
    {
        Animator.SetBool("IsAttacking", false);
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