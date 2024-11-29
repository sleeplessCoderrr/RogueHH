using System;
using UnityEngine;

public abstract class BaseState<TState> where TState : Enum
{
    protected readonly StateManager<TState> StateManager;
    protected readonly Animator Animator;
    public TState StateKey { get; private set; }

    protected BaseState(StateManager<TState> stateManager, Animator animator,TState key)
    {
        this.StateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
        this.StateKey = key;
        this.Animator = animator ?? throw new ArgumentNullException(nameof(animator));
    }
    
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract TState GetNextState();
}