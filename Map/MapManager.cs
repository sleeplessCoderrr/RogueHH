using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    
    [Header("Map Settings")]
    public MapConfig mapConfig;
    public MapData mapData;
    
    [Header("Builder")]
    private FloorDecorationBuilder _floorDecorationBuilder;
    private RoomDecorationBuilder _roomDecorationBuilder;
    private TunnelBuilder _tunnelBuilder;
    private RoomBuilder _roomBuilder;
    private KruskalMST _kruskalMst;
    
    private async void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        _kruskalMst = new KruskalMST();
        _roomBuilder = new RoomBuilder();
        _tunnelBuilder = new TunnelBuilder();
        _roomDecorationBuilder = new RoomDecorationBuilder();
        _floorDecorationBuilder = new FloorDecorationBuilder();
        
        MakeRooms();

        await Task.Delay(1000);
        GetData();
        MakeTunnels();
        MakeDecorations();
        _roomBuilder.Build();
    }
    private void MakeRooms()
    {
        _roomBuilder.SetParent(transform);
        _roomBuilder.SetPrefab(new GameObject[] { mapConfig.floorTile });

        _roomBuilder.SetDimensions(mapConfig.width, mapConfig.height);
        _roomBuilder.InitializeGrid();
        
        mapData.MapTileData = _roomBuilder
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
        _tunnelBuilder.InitializeGrid();
        _tunnelBuilder.SetParent(transform);
        _tunnelBuilder.AddTunnel(mapData.MstEdges);
    }

    private void MakeDecorations()
    {
        _floorDecorationBuilder.SetParent(transform);
        _roomDecorationBuilder.SetParent(transform);
        
        _floorDecorationBuilder.SetPrefab(mapConfig.floorDecorations);
        _roomDecorationBuilder.SetPrefab(mapConfig.roomDecorations);
        
        _floorDecorationBuilder.InitializeGrid();
        _roomDecorationBuilder.InitializeGrid();

        _floorDecorationBuilder.AddFloorDecoration();
        _roomDecorationBuilder.AddRoomDecoration(mapData.Rooms);
    }

    private void GetData()
    {
        mapData.Rooms = _roomBuilder.GetRooms();
        mapData.roomCenters = MapUtility.GetAllRoomCenters(mapData.Rooms);
        mapData.MstEdges = _kruskalMst.Compute(mapData.roomCenters);
    }

    
}


