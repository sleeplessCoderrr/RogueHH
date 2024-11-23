using System;
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
    [Header("Player Config & Data")]
    public PlayerConfig playerConfig;
    public PlayerData playerData;
    private Player _player;
    
    [Header("Importing Map Data")]
    public MapData mapData;
    public MapConfig mapConfig;

    private PlayerBuilder _playerBuilder;

    private void Awake()
    {
        _player = new Player(playerConfig, playerData);
        _playerBuilder = new PlayerBuilder();
    }

    private async void Start()
    {
        //Delay 3 Seconds
        await Task.Delay(3000);
        
        GetMapData();
        Initialize();
    }

    private void Initialize()
    {
        _playerBuilder
            .SetData(_player.PlayerConfig, _player.PlayerData)
            .InitializeRandomPosition(mapConfig, mapData);
    }

    private void GetMapData()
    {
        mapData = MapManager.Instance.mapData;
        mapConfig = MapManager.Instance.mapConfig;
    }
}