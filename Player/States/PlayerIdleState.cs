using System;
using UnityEngine;

public class PlayerIdleState : BaseState<PlayerState>
{
    public PlayerIdleState(StateManager<PlayerState> stateManager, Animator animator, PlayerState key) : base(stateManager, animator, key)
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
        return StateKey;
    }
}