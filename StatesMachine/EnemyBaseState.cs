using UnityEngine;
using System.Collections.Generic;

public abstract class EnemyBaseState
{
    protected Enemy Enemy;
    protected Animator Animator;

    protected EnemyBaseState(Enemy enemy, Animator animator)
    {
        Enemy = enemy;
        Animator = animator;
    }
    
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract PlayerState? GetNextState(); 
}