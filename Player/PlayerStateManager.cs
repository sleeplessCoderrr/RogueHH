using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking,
    Attack
}

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance { get; private set; }

    private Player _player; 
    private Animator _animator;
    private PlayerStateBase _currentState;
    private Dictionary<PlayerState, PlayerStateBase> _states;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        InitializeStates();
    }
    
    private void InitializeStates()
    {
        _animator = GetComponent<Animator>();
        _states = new Dictionary<PlayerState, PlayerStateBase>
        {
            { PlayerState.Idle, new PlayerIdleState(_player, _animator) },
            { PlayerState.Walking, new PlayerWalkingState(_player, _animator) },
            { PlayerState.Attack, new PlayerAttackState(_player, _animator) }
        };
        _currentState = _states[PlayerState.Idle];
    }

    public void SetState(PlayerState newState)
    {
        _currentState.ExitState();
        _currentState = _states[newState];
        _currentState.EnterState();
    }

    public void SetPlayerEntity(Player entity)
    {
        _player = entity;
        InitializeStates(); 
    }
}
