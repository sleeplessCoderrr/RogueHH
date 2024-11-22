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
            .AddRandomRooms(8, 8, 8, 12, 12) // Generate 5 random rooms
            .Build();

        _rooms = _mapBuilder.GetRooms();
        _roomCenters = GetAllRoomCenters();

        //A* and MST pathfinding
        var mstEdges = ComputeMst(_roomCenters);
        foreach (var edge in mstEdges)
        {
            var path = AStarPathfinder.FindPath(_mapGrid, edge.Item1, edge.Item2);
            CreateTunnel(path);
        }
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
    
    private List<(Vector2Int, Vector2Int)> ComputeMst(List<Vector2Int> roomCenters)
    {
        List<(Vector2Int, Vector2Int)> mstEdges = new List<(Vector2Int, Vector2Int)>();
        HashSet<int> visited = new HashSet<int>(); // To track visited rooms
        List<Vector2Int> unvisited = new List<Vector2Int>(roomCenters); // List of unvisited rooms

        // Ensure there are rooms before accessing
        if (roomCenters.Count == 0)
        {
            Debug.LogError("No rooms available for MST calculation.");
            return mstEdges; // Early exit if no rooms
        }

        // Start from the first room center
        visited.Add(0);
        unvisited.RemoveAt(0); // Remove the first room from unvisited list

        while (visited.Count < roomCenters.Count)
        {
            float minDistance = float.MaxValue;
            int selectedRoom1 = -1, selectedRoom2 = -1;

            // Look for the closest edge between visited and unvisited rooms
            foreach (int visitedIndex in visited)
            {
                for (int unvisitedIndex = 0; unvisitedIndex < unvisited.Count; unvisitedIndex++)
                {
                    // Calculate distance between room centers
                    float distance = Vector2Int.Distance(roomCenters[visitedIndex], unvisited[unvisitedIndex]);

                    // If the distance is smaller, update the closest edge
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        selectedRoom1 = visitedIndex;
                        selectedRoom2 = unvisitedIndex;
                    }
                }
            }

            // Ensure valid indices before accessing
            if (selectedRoom1 != -1 && selectedRoom2 != -1)
            {
                // Add the closest edge to the MST and update sets
                mstEdges.Add((roomCenters[selectedRoom1], roomCenters[selectedRoom2]));
                visited.Add(selectedRoom2); // Mark the selected room as visited
                unvisited.RemoveAt(selectedRoom2); // Remove it from unvisited list
            }
            else
            {
                Debug.LogWarning("Failed to find a valid room connection.");
                break; // Exit loop if no valid edge is found
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


