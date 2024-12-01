using System;
using UnityEngine;

public abstract class BaseState<TState, TEntity> where TState : Enum
{
    protected readonly StateManager<TState, TEntity> StateManager;
    protected readonly Animator Animator;
    protected readonly TEntity Entity; 
    public TState StateKey { get; private set; }

    protected BaseState(StateManager<TState, TEntity> stateManager, Animator animator, TState key, TEntity entity)
    {
        StateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
        StateKey = key;
        Animator = animator ?? throw new ArgumentNullException(nameof(animator));
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract TState GetNextState();
}