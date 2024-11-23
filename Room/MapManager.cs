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
    private KruskalMST _kruskalMst;

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
        _kruskalMst = new KruskalMST();
        _mapBuilder = new MapBuilder();
        
        MakeRooms();
        GetData();
        MakeTunnels();
    }
    private void MakeRooms()
    {
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
    }

    private void MakeTunnels()
    {
        _mapBuilder.AddTunnel(mapData.MstEdges);
        _mapBuilder.Build();
    }

    private void GetData()
    {
        mapData.Rooms = _mapBuilder.GetRooms();
        mapData.roomCenters = _mapBuilder.GetAllRoomCenters(mapData.Rooms);
        mapData.MstEdges = _kruskalMst.Compute(mapData.roomCenters);
    }

    
}


