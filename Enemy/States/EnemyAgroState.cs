using UnityEngine;

public class EnemyAgroState : EnemyBaseState
{
    public EnemyAgroState(Enemy enemy, Animator animator) : base(enemy, animator)
    {
    }

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
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