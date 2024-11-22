using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject floorTile;
    private Tile[,] _mapGrid;
    private List<Vector2Int> _roomCenters = new List<Vector2Int>();
    private List<Room> _rooms;
    private MapBuilder _mapBuilder;

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
        var mstEdges = ComputeKruskalMst(_roomCenters);
        foreach (var edge in mstEdges)
        {
            _mapBuilder.AddTunnelBetweenPoints(edge.Item1, edge.Item2);
            var path = AStarPathfinder.FindPath(_mapGrid, edge.Item1, edge.Item2);
            CreateTunnel(path);
        }
        Debug.Log("Ended");
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
    
    private List<(Vector2Int, Vector2Int)> ComputeKruskalMst(List<Vector2Int> roomCenters)
    {
        var mstEdges = new List<(Vector2Int, Vector2Int)>();
        var edges = new List<(float, Vector2Int, Vector2Int)>();

        // Generate all edges with distances
        for (int i = 0; i < roomCenters.Count; i++)
        {
            for (int j = i + 1; j < roomCenters.Count; j++)
            {
                float distance = Vector2Int.Distance(roomCenters[i], roomCenters[j]);
                edges.Add((distance, roomCenters[i], roomCenters[j]));
            }
        }

        // Sort edges by distance
        edges.Sort((a, b) => a.Item1.CompareTo(b.Item1));

        // Initialize Union-Find structure
        var parent = new Dictionary<Vector2Int, Vector2Int>();
        foreach (var room in roomCenters)
        {
            parent[room] = room; // Each room starts as its own parent
        }

        // Find root of a set (with path compression)
        Vector2Int Find(Vector2Int node)
        {
            if (parent[node] != node)
            {
                parent[node] = Find(parent[node]);
            }
            return parent[node];
        }

        // Union two sets
        void Union(Vector2Int a, Vector2Int b)
        {
            var rootA = Find(a);
            var rootB = Find(b);
            if (rootA != rootB)
            {
                parent[rootB] = rootA;
            }
        }

        // Add edges to the MST
        foreach (var (distance, room1, room2) in edges)
        {
            if (Find(room1) != Find(room2)) // Avoid cycles
            {
                mstEdges.Add((room1, room2));
                Union(room1, room2);

                // Stop if we have enough edges for an MST
            }
            if (mstEdges.Count == roomCenters.Count - 1)
            {
                    break;
            }
            }

        return mstEdges;
    }

    private void CreateTunnel(List<Vector2Int> path)
    {
        foreach (var point in path)
        {
            // Ensure that the tile at this point is walkable
            _mapGrid[point.x, point.y].IsWalkable = true;
            _mapBuilder.Build();
            // Optionally, you can add other actions, like marking the tile with specific tunnel graphics
            // or other features if you wish to distinguish tunnels from regular walkable tiles.
        }
        
    }
}


