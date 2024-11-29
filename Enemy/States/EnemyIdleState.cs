using UnityEngine;

public class EnemyIdleState : BaseState<EnemyState>
{
    public EnemyIdleState(StateManager<EnemyState> stateManager, Animator animator, EnemyState key) : base(stateManager, animator, key) {}

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override EnemyState GetNextState()
    {
        throw new System.NotImplementedException();
    }
}