using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<TState> : MonoBehaviour where TState : Enum
{
    protected Dictionary<TState, BaseState<TState>> States = new Dictionary<TState, BaseState<TState>>();
    protected BaseState<TState> CurrentState;

    protected void Start()
    {
        if (CurrentState == null)
            throw new InvalidOperationException("CurrentState is not set!");

        CurrentState.EnterState();
    }

    protected void Update()
    {
        if (CurrentState == null)
            return;

        var nextState = CurrentState.GetNextState();
        if (!nextState.Equals(CurrentState.StateKey))
        {
            TransitionState(nextState);
        }
        else
        {
            CurrentState.UpdateState();
        }
    }

    private void TransitionState(TState newStateKey)
    {
        if (!States.ContainsKey(newStateKey))
            throw new InvalidOperationException($"State {newStateKey} is not defined!");

        CurrentState.ExitState();
        CurrentState = States[newStateKey];
        CurrentState.EnterState();
    }

    protected abstract void InitializeStates();
}