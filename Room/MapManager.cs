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
                    GameObject mapManager = new GameObject("MapManager");
                    _instance = mapManager.AddComponent<MapManager>();
                }
            }
            return _instance;
        }
    }
    
    [Header("Map Settings Data")]
    public RoomConfig roomConfig;
    public TunnelConfig tunnelConfig;
    
    private RoomBuilder _roomBuilder;
    private TunnelBuilder _tunnelBuilder;
    
    private MapGraph _mapGraph;

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
        _roomBuilder = new RoomBuilder(roomConfig);
        _tunnelBuilder = new TunnelBuilder(tunnelConfig);
        GenerateMap();
    }

    private void GenerateMap()
    {   
        _mapGraph = new MapGraph();
        _roomBuilder.Build();
    }
}
