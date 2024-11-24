using System;
using UnityEngine;

public class PlayerWalkingState : BaseState<PlayerState>
{
    public PlayerWalkingState(StateManager<PlayerState> stateManager, PlayerState key) : base(stateManager, key)
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
