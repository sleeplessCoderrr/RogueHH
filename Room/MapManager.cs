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
    public MapConfig mapConfig;
    public RoomConfig roomConfig;
    public TunnelConfig tunnelConfig;
    
    [Header("Map Data")]
    private Map _map;
    
    //Builder
    private RoomBuilder _roomBuilder;
    private TunnelBuilder _tunnelBuilder;
    private HelperUtils _utils = new HelperUtils();
    
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
        _map = new Map(mapConfig);
        
        GenerateMap();
    }

    private void GenerateMap()
    {   
        //Generating random number of rooms
        int numberOfRooms = Random.Range(5, 10);
        for (int i = 0; i < numberOfRooms; i++)
        {
            Vector3 randomPosition = _utils.GenerateRandomPosition();
            CreateRoomAtPosition(randomPosition);
        }
    }

    private void CreateRoomAtPosition(Vector3 position)
    {
        _roomBuilder.Build(position);
    }

    
    
}
