using System.Collections.Generic;
using UnityEngine;

public class Builder
{
    private int _width, _height;
    private Tile[,] _grid;
    private GameObject _floorPrefab; // Single prefab for all floor tiles
    private Transform _parentTransform;

    public Builder SetDimensions(int width, int height)
    {
        this._width = width;
        this._height = height;
        return this;
    }

    public Builder SetParent(Transform parent)
    {
        this._parentTransform = parent;
        return this;
    }

    public Builder SetPrefab(GameObject floorPrefab)
    {
        this._floorPrefab = floorPrefab;
        return this;
    }

    public Builder InitializeGrid()
    {
        _grid = new Tile[_width, _height];
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                _grid[x, y] = new Tile(x, y, false); // Default: not walkable (no floor)
            }
        }
        return this;
    }

    public Builder AddRoom(int startX, int startY, int roomWidth, int roomHeight)
    {
        for (var x = startX; x < startX + roomWidth; x++)
        {
            for (var y = startY; y < startY + roomHeight; y++)
            {
                _grid[x, y].IsWalkable = true; // Mark tiles as walkable
            }
        }
        return this;
    }

    public Builder AddTunnelBetweenPoints(Vector2Int start, Vector2Int end)
    {
        var current = start;

        // Tunnel horizontally first
        while (current.x != end.x)
        {
            var direction = end.x > current.x ? 1 : -1;
            current.x += direction;
            _grid[current.x, current.y].IsWalkable = true; // Mark as walkable
        }

        // Then tunnel vertically
        while (current.y != end.y)
        {
            var direction = end.y > current.y ? 1 : -1;
            current.y += direction;
            _grid[current.x, current.y].IsWalkable = true; // Mark as walkable
        }

        return this;
    }

    public Tile[,] Build()
    {
        // Instantiate only the walkable tiles (floors)
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (_grid[x, y].IsWalkable)
                {
                    var position = new Vector3(x, 0, y); // 3D placement
                    Object.Instantiate(_floorPrefab, position, Quaternion.identity, _parentTransform);
                }
            }
        }

        return _grid;
    }
}
