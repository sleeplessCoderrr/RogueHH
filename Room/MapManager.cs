using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject floorTile;
    private Tile[,] _mapGrid;
    
    private void Start()
    {
        var mapBuilder = new Builder()
            .SetDimensions(50, 50)
            .SetParent(transform)
            .SetPrefab(floorTile)
            .InitializeGrid()
            .AddRoom(5, 5, 8, 8) // Example room
            .AddRoom(20, 20, 6, 6) // Example room
            .AddTunnelBetweenPoints(new Vector2Int(9, 9), new Vector2Int(20, 20)) // Example tunnel
            .Build();

        _mapGrid = mapBuilder;

        // .AddRoom(2, 2, 5, 3)
        // .AddTunnel(7, 3, 6, true)
        // .AddRoom(13, 2, 4, 4)
        // .AddTunnel(16, 6, 4, false);
    }
}
