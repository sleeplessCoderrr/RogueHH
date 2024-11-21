using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Map
{
    private List<List<Tile>> _tiles;
    public MapConfigSO MapConfig;
 
    public Map(MapConfigSO mapConfig)
    {
        this.MapConfig = mapConfig;
        InitializeMap();
    }

    private void InitializeMap()
    {   
        _tiles = new List<List<Tile>>();
        for (var x=0; x < MapConfig.widthX; x++)
        {   
            var row = new List<Tile>();
            for (var z = 0; z < MapConfig.widthY; z++)
            {
                var tilePos = new Vector3(x, 0, z);
                row.Add(new Tile(tilePos, false));
            }
            _tiles.Add(row);
        }
    }

    public Tile GetTile(int x, int z)
    {
        if (x >= 0 && x < MapConfig.widthX && z >= 0 && z < MapConfig.widthY)
        {
            return _tiles[x][z];
        }
        return null;
    }

    public void SetTile(int x, int z, Tile tile)
    {
        if (x >= 0 && x < MapConfig.widthX && z >= 0 && z < MapConfig.widthY)
        {
            _tiles[x][z] = tile;
        }
    }
    
    public List<Tile> GetNeighbors(Tile tile)
    {
        var neighbors = new List<Tile>();
        Vector3[] directions = {
            Vector3.forward, Vector3.back, Vector3.left, Vector3.right,
            new Vector3(1, 0, 1), new Vector3(1, 0, -1), new Vector3(-1, 0, 1), new Vector3(-1, 0, -1)
        };
        
        foreach (var dir in directions)
        {
            var neighbor = GetTile(
                Mathf.RoundToInt(tile.Position.x + dir.x),
                Mathf.RoundToInt(tile.Position.z + dir.z)
            );
            
            if (neighbor != null && neighbor.IsWalkable)
                neighbors.Add(neighbor);
        }

        return neighbors;
    }
    
}
