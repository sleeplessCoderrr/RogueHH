using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    
    [Header("Map Settings")]
    public MapConfig mapConfig;
    public MapData mapData;
    
    [Header("Factory")]
    private MapBuilder _mapBuilder;
    private KruskalMST _kruskalMst = new KruskalMST();

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
        mapData.MapTileData = _mapBuilder
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

        mapData.Rooms = _mapBuilder.GetRooms();
        mapData.roomCenters = _mapBuilder.GetAllRoomCenters(mapData.Rooms);

        var mstEdges = _kruskalMst.Compute(mapData.roomCenters);
        foreach (var edge in mstEdges)
        {
            _mapBuilder.AddTunnelBetweenPoints(edge.Item1, edge.Item2);
        }
        
        _mapBuilder.Build();
        
        //AStar for the movement
        // var path = AStarPathfinder.FindPath(_mapGrid, edge.Item1, edge.Item2);
    }
}


