using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking
}

public class PlayerStateManager : StateManager<PlayerState>
{
    public static PlayerStateManager Instance;
    
    [Header("Player Config & Data")]
    public PlayerConfig playerConfig;
    public PlayerData playerData;
    
    [Header("Importing Map Data")]
    public MapConfig mapConfig;
    public MapData mapData;
    
    private Player _player;
    private PlayerBuilder _playerBuilder;
    
    private Animator _animator;
    private GameObject _playerInstance;
    
    private async void Awake()
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
        
        InitializePlayer();
        await Task.Delay(1000);
        SetAnimator();
        InitializeStates();
        CurrentState = States[PlayerState.Idle];
    }
    
    protected override void InitializeStates()
    {
        States[PlayerState.Idle] = new PlayerIdleState(this, _animator, PlayerState.Idle);
        States[PlayerState.Walking] = new PlayerWalkingState(this, _animator,PlayerState.Walking);
    }

    private async void InitializePlayer()
    {
        await Task.Delay(1000);
        _player = new Player(playerConfig, playerData);
        _playerBuilder = new PlayerBuilder();
        
        _playerInstance = _playerBuilder
            .SetParent(transform)
            .SetData(_player.PlayerConfig, _player.PlayerData)
            .InitializeRandomPosition(mapConfig, mapData);
    }

    private void SetAnimator()
    {
        _animator = _playerInstance.GetComponent<Animator>();
    }
}

