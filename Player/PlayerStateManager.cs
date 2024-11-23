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
    public static PlayerStateManager Instance;
    
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
    }

    private async void Start()
    {
        //Delay 1 Seconds
        await Task.Delay(1000);
        
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