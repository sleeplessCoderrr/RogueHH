using System;
using UnityEngine;

public class PlayerWalkingState : BaseState<PlayerState, Player>
{
    public PlayerWalkingState(StateManager<PlayerState, Player> stateManager, Animator animator, PlayerState key, Player entity) : base(stateManager, animator, key, entity)
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
