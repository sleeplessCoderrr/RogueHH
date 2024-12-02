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
    Attack
}

public class EnemyStateManager : MonoBehaviour
{
    public Enemy _enemy;
    private EnemyBaseState _currentState;
    private Dictionary<EnemyState, EnemyBaseState> _states;
    private Animator _animator;
    
    private void Start()
    {
        InitializeStates();
    }

    private void InitializeStates()
    {
        _animator = GetComponent<Animator>();
        _states = new Dictionary<EnemyState, EnemyBaseState>
        {
            { EnemyState.Idle, new EnemyIdleState(_enemy, _animator) },
            { EnemyState.Alert, new EnemyAlertState(_enemy, _animator) },
        };
        _currentState = _states[EnemyState.Idle];
    }

    public void SetState(EnemyState state)
    {
        _currentState.ExitState();
        _currentState = _states[state];
        _currentState.EnterState();
    }

    public void SetEnemyEntity(Enemy entity)
    {
        _enemy = entity;
        InitializeStates();
    }
}