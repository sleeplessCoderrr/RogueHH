using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    
    [Header("Map Settings")]
    public MapConfig mapConfig;
    private Tile[,] _mapGrid;
    private List<Room> _rooms;
    private MapBuilder _mapBuilder;
    private KruskalMST _kruskalMst = new KruskalMST();
    private List<Vector2Int> _roomCenters = new List<Vector2Int>();

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
    }
    
    private void Start()
    {
        _mapBuilder = new MapBuilder();
        _mapGrid = _mapBuilder
            .SetDimensions(mapConfig.width, mapConfig.height)
            .SetParent(transform)
            .SetPrefab(mapConfig.floorTile)
            .InitializeGrid()
            .AddRandomRooms(
                Random.Range(8, 12), 
                        mapConfig.minWidth, 
                        mapConfig.minHeight, 
                        mapConfig.maxWidth, 
                        mapConfig.maxHeight)
            .Build();

        _rooms = _mapBuilder.GetRooms();
        _roomCenters = _mapBuilder.GetAllRoomCenters(_rooms);

        var mstEdges = _kruskalMst.Compute(_roomCenters);
        foreach (var edge in mstEdges)
        {
            _mapBuilder.AddTunnelBetweenPoints(edge.Item1, edge.Item2);
        }
        
        _mapBuilder.Build();
        
        //AStar for the movement
        // var path = AStarPathfinder.FindPath(_mapGrid, edge.Item1, edge.Item2);
    }
}


