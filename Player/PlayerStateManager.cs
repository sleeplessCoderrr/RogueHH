﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    private PlayerBuilder _playerBuilder;
    private Player _player;
    
    private Animator _animator;
    public GameObject playerInstance;
    
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
        
        playerInstance = _playerBuilder
            .SetParent(transform)
            .SetData(_player.PlayerConfig, _player.PlayerData)
            .InitializeRandomPosition(mapConfig, mapData);
    }

    private void SetAnimator()
    {
        _animator = playerInstance.GetComponent<Animator>();
    }
}

