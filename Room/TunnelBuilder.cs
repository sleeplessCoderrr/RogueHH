using UnityEngine;
using System.Collections.Generic;

public class TunnelBuilder : BaseBuilder
{
    public override void InitializeGrid()
    {
        Grid = MapManager.Instance.mapData.MapTileData;
    }
    
    public TunnelBuilder AddTunnel(List<(Vector2Int, Vector2Int)> mstEdges)
    {
        foreach (var edge in mstEdges)
        {
            AddTunnelBetweenPoints(edge.Item1, edge.Item2);
        }

        return this;
    }
    
    private TunnelBuilder AddTunnelBetweenPoints(Vector2Int start, Vector2Int end)
    {
        var current = start;
        while (current.x != end.x)
        {
            var direction = end.x > current.x ? 1 : -1;
            current.x += direction;
            Grid[current.x, current.y].IsRoom = true; 
            Grid[current.x, current.y].IsTunnelPath = true; 
        }

        // Then tunnel vertically
        while (current.y != end.y)
        {
            var direction = end.y > current.y ? 1 : -1;
            current.y += direction;
            Grid[current.x, current.y].IsRoom = true; 
            Grid[current.x, current.y].IsTunnelPath = true; 
        }

        return this;
    }
}