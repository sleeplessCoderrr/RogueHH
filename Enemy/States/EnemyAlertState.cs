using UnityEngine;

public class EnemyAlertState : EnemyBaseState
{
    public EnemyAlertState(Enemy enemy, Animator animator) : base(enemy, animator)
    {
    }

    public override void EnterState()
    {
        Animator.SetBool("IsAlert", true);
        
    }

    public override void ExitState()
    {
        Animator.SetBool("IsAlert", false);
    }

    public override void UpdateState()
    {
        // Additional Alert-specific logic if needed
    }

    public override PlayerState? GetNextState()
    {
        return null;
    }
}