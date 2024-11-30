using System.Threading.Tasks;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking
}

public class PlayerStateManager : StateManager<PlayerState, Player>
{
    public PlayerStateManager(Player entity) : base(entity)
    {
        InitializeStates();
    }

    protected override void InitializeStates()
    {
        States[PlayerState.Idle] = new PlayerIdleState(this, Entity.Animator, PlayerState.Idle, Entity);
        States[PlayerState.Walking] = new PlayerWalkingState(this, Entity.Animator, PlayerState.Walking, Entity);
    }
}

