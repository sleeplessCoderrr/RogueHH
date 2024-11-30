using UnityEngine;

public class EnemyIdleState : BaseState<EnemyState, Enemy>
{
    public EnemyIdleState(StateManager<EnemyState, Enemy> stateManager, Animator animator, EnemyState key, Enemy entity) : base(stateManager, animator, key, entity)
    {
    }

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