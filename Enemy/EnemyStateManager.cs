using UnityEngine;

public enum EnemyState
{
    Idle,
    Walk
}

public class EnemyStateManager : StateManager<EnemyState>
{
    protected override void InitializeStates()
    {
    }
}