using System;
using UnityEngine;

public class PlayerIdleState : BaseState<PlayerState>
{
    public PlayerIdleState(StateManager<PlayerState> stateManager, PlayerState key) : base(stateManager, key)
    {
        
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    public override PlayerState GetNextState()
    {
        throw new NotImplementedException();
    }
}