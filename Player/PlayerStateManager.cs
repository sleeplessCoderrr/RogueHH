using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using States;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking
}

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;
    
    [Header("Player Config & Data")]
    public PlayerConfig playerConfig;
    public PlayerData playerData;
    
    private Player _player;
    private PlayerBuilder _playerBuilder;
    
    public Dictionary<PlayerState, BaseState<PlayerState>> States { get; private set; }
    public BaseState<PlayerState> CurrentState { get; private set; }
    
    [Header("Importing Map Data")]
    public MapData mapData;
    public MapConfig mapConfig;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
        _player = new Player(playerConfig, playerData);
        _playerBuilder = new PlayerBuilder();
        Initialize();
    }

    private async void Start()
    {
        //Delay 1 Seconds
        await Task.Delay(1000);
        
        GetMapData();
        _playerBuilder
            .SetData(_player.PlayerConfig, _player.PlayerData)
            .InitializeRandomPosition(mapConfig, mapData);
    }

    private void Initialize()
    {
        States = new Dictionary<PlayerState, BaseState<PlayerState>>
        {
            { PlayerState.Idle, new PlayerIdleState(this, PlayerState.Idle) },
            { PlayerState.Walking, new PlayerWalkingState(this, PlayerState.Walking) }
        };

        // Set initial state
        CurrentState = States[PlayerState.Idle];
    }
    
    private void Update()
    {
        if (CurrentState == null) return;

        PlayerState nextState = CurrentState.GetNextState();
        if (nextState != CurrentState.StateKey)
        {
            TransitionTo(nextState);
        }
        else
        {
            CurrentState.UpdateState();
        }
    }

    private void TransitionTo(PlayerState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState();
        }

        CurrentState = States[newState];
        CurrentState.EnterState();
    }

    private void GetMapData()
    {
        mapData = MapManager.Instance.mapData;
        mapConfig = MapManager.Instance.mapConfig;
    }
}