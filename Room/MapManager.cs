using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;

    private static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapManager>();
                if (_instance == null)
                {
                    var mapManager = new GameObject("MapManager");
                    _instance = mapManager.AddComponent<MapManager>();
                }
            }
            return _instance;
        }
    }
    
    [Header("Map Settings Data")]
    public MapConfigSO mapConfig;
    public RoomConfigSO roomConfig;
    public TunnelConfigSO tunnelConfig;
    
    [Header("Map Data")]
    private Map _currentMap;
    
    //Builder
    private RoomBuilder _roomBuilder;
    private TunnelBuilder _tunnelBuilder;
    private HelperUtils _utils;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (_instance != this)
        {
            Destroy(gameObject); 
        }
    }
    
    private void Start()
    {
        if (mapConfig != null
            && roomConfig != null
            && tunnelConfig != null)
        {
            GenerateMap();
        }
        else
        {
            Debug.LogError("Config is not assigned!");
        }
    }
    
    private void GenerateMap()
    {
        _utils = new HelperUtils(mapConfig);
        _currentMap = new Map(mapConfig);
        _roomBuilder = new RoomBuilder(
            roomConfig, 
            mapConfig.floorTile, 
            mapConfig.floorDecorations, 
            mapConfig.roomDecorations);
        _tunnelBuilder = new TunnelBuilder(tunnelConfig);
        
        GenerateRandomRooms();
    }

    private void CreateGameObject()
    { 
        
    }

    private void GenerateRandomRooms()
    { 
        _roomBuilder.Build(new Vector3(0, 5, 0));   
    }

    public Map GetMap()
    {
        return _currentMap;
    }
}
