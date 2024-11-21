using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Map
{
    private List<List<Tile>> _tiles;
    public MapConfig MapConfig;

    public Map(MapConfig mapConfig)
    {
        this.MapConfig = mapConfig;
        InitializeMap();
    }

    private void InitializeMap()
    {   
        _tiles = new List<List<Tile>>();
        for (int x=0; x < MapConfig.widthX; x++)
        {   
            List<Tile> row = new List<Tile>();
            for (int z = 0; z < MapConfig.widthY; z++)
            {
                Vector3 tilePos = new Vector3(x, 0, z);
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
    
}
