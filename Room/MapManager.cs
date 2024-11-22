using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Prefabs for Floor")]
    public GameObject floorTile;
    private Tile[,] _mapGrid;
    private List<Room> _rooms;
    private MapBuilder _mapBuilder;
    private List<Vector2Int> _roomCenters = new List<Vector2Int>();
    private KruskalMST _kruskalMst = new KruskalMST();
    
    private void Start()
    {
        _mapBuilder = new MapBuilder();
        _mapGrid = _mapBuilder
            .SetDimensions(100, 100)
            .SetParent(transform)
            .SetPrefab(floorTile)
            .InitializeGrid()
            .AddRandomRooms(8, 8, 8, 12, 12)
            .Build();

        _rooms = _mapBuilder.GetRooms();
        _roomCenters = GetAllRoomCenters();

        //A* and MST pathfinding
        var mstEdges = _kruskalMst.Compute(_roomCenters);
        foreach (var edge in mstEdges)
        {
            _mapBuilder.AddTunnelBetweenPoints(edge.Item1, edge.Item2);
            
            //To be Search Again
            //===================
            // var path = AStarPathfinder.FindPath(_mapGrid, edge.Item1, edge.Item2);
            // CreateTunnel(path);
        }
        _mapBuilder.Build();
       
    }
    
    private List<Vector2Int> GetAllRoomCenters()
    {
        var roomCenters = new List<Vector2Int>();
        foreach (var room in _rooms)
        {
            var center = room.GetCenter();
            roomCenters.Add(center);
        }

        return roomCenters;
    }

    private void CreateTunnel(List<Vector2Int> path)
    {
        foreach (var point in path)
        {
            _mapGrid[point.x, point.y].IsWalkable = true;
            _mapBuilder.Build();
        }
    }
}


