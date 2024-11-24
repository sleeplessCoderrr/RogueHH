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
    
    private Player _player;
    private PlayerBuilder _playerBuilder;

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
        InitializeStates();
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


    protected override void InitializeStates()
    {
        States[PlayerState.Idle] = new PlayerIdleState(this, PlayerState.Idle);
        States[PlayerState.Walking] = new PlayerIdleState(this, PlayerState.Walking);
    }
    
    private void GetMapData()
    {
        mapData = MapManager.Instance.mapData;
        mapConfig = MapManager.Instance.mapConfig;
    }
}