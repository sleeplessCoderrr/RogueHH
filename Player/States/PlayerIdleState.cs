using System;
using UnityEngine;

public class PlayerIdleState : BaseState<PlayerState, Player>
{
    public PlayerIdleState(StateManager<PlayerState, Player> stateManager, Animator animator, PlayerState key, Player entity) : base(stateManager, animator, key, entity)
    {
    }

    public override void EnterState()
    {
        throw new NotImplementedException();
    }

    public override void ExitState()
    {
        throw new NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new NotImplementedException();
    }

    public override PlayerState GetNextState()
    {
        throw new NotImplementedException();
    }
}