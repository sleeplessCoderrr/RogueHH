using System;
using UnityEngine;

public abstract class BaseState<TState> where TState : Enum
{
    protected StateManager<TState> StateManager;
    public TState StateKey { get; private set; }

    public BaseState(StateManager<TState> stateManager, TState key)
    {
        StateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
        StateKey = key;
    }
    
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract TState GetNextState();
}