using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class StateManager<TState, TEntity> : MonoBehaviour where TState : Enum
{
    protected Dictionary<TState, BaseState<TState, TEntity>> States = new Dictionary<TState, BaseState<TState, TEntity>>();
    protected BaseState<TState, TEntity> CurrentState;
    protected TEntity Entity;

    protected StateManager(TEntity entity)
    {
        Entity = entity;
    }
    
    protected async void Start()
    {
        await Task.Delay(3000);
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
