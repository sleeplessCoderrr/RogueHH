using System.Collections.Generic;
using UnityEditor.VersionControl;
using System.Threading.Tasks;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public enum EnemyState
{
    Idle,
    Alert,
    Aggro,
    Walk,
    Attack
}

public class EnemyStateManager : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;
    private EnemyBaseState _stateInvoker;
    private Dictionary<EnemyState, EnemyBaseState> _states;

    public EnemyState currentState;

    private void Start()
    {
        InitializeStates();
    }

    private void InitializeStates()
    {
        _animator = gameObject.GetComponent<Animator>();
        _states = new Dictionary<EnemyState, EnemyBaseState>
        {
            { EnemyState.Idle, new EnemyIdleState(_enemy, _animator) },
            { EnemyState.Alert, new EnemyAlertState(_enemy, _animator) },
            { EnemyState.Aggro, new EnemyAgroState(_enemy, _animator) },
            { EnemyState.Walk , new EnemyWalkingState(_enemy, _animator)}
        };
        _stateInvoker = _states[EnemyState.Idle];
        _stateInvoker.EnterState();
        currentState = EnemyState.Idle;
    }

    public void SetState(EnemyState state)
    {
        if (_stateInvoker != null)
            _stateInvoker.ExitState();

        _stateInvoker = _states[state];
        _stateInvoker.EnterState();
        
        currentState = state;
    }
    
    public void SetEnemyEntity(Enemy entity)
    {
        _enemy = entity;
        InitializeStates();
    }
}
