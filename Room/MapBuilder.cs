using UnityEngine;
using System.Collections.Generic;

public class MapBuilder
{
    private int _width, _height;
    private Tile[,] _grid;
    private GameObject _floorPrefab;
    private Transform _parentTransform;
    private List<Room> _rooms  = new List<Room>();

    public MapBuilder SetDimensions(int width, int height)
    {
        this._width = width;
        this._height = height;
        return this;
    }

    public MapBuilder SetParent(Transform parent)
    {
        this._parentTransform = parent;
        return this;
    }

    public MapBuilder SetPrefab(GameObject floorPrefab)
    {
        this._floorPrefab = floorPrefab;
        return this;
    }

    public MapBuilder InitializeGrid()
    {
        _grid = new Tile[_width, _height];
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                _grid[x, y] = new Tile(x, y, false);
            }
        }
        return this;
    }
    
    private bool IsValidRoomPosition(int startX, int startY, int roomWidth, int roomHeight)
    {
        if (startX < 0 || startY < 0 || startX + roomWidth > _width || startY + roomHeight > _height)
        {
            return false;
        }

        const int buffer = 10;
        for (var x = startX - buffer; x < startX + roomWidth + buffer; x++)
        {
            for (var y = startY - buffer; y < startY + roomHeight + buffer; y++)
            {
                if (x < 0 || y < 0 || x >= _width || y >= _height)
                {
                    continue;
                }

                if (_grid[x, y].IsWalkable)
                {
                    return false;
                }
            }
        }

        return true; // Valid position
    }
    
    private MapBuilder AddRoom(int startX, int startY, int roomWidth, int roomHeight)
    {
        if (!IsValidRoomPosition(startX, startY, roomWidth, roomHeight))
        {
            return this;
        }
        
        var room = new Room(startX, startY, roomWidth, roomHeight);
        for (var x = startX; x < startX + roomWidth; x++)
        {
            for (var y = startY; y < startY + roomHeight; y++)
            {
                _grid[x, y].IsWalkable = true;
            }
        }

        _rooms.Add(room); 
        return this;
    }

    public MapBuilder AddRandomRooms(int roomCount, int minWidth, int minHeight, int maxWidth, int maxHeight)
    {
        var rand = new System.Random();
        for (var i = 0; i < roomCount; i++)
        {
            var roomAdded = false;
            while (!roomAdded)
            {
                var roomWidth = rand.Next(minWidth, maxWidth);
                var roomHeight = rand.Next(minHeight, maxHeight);
                var startX = rand.Next(0, _width - roomWidth);
                var startY = rand.Next(0, _height - roomHeight);

                if (IsValidRoomPosition(startX, startY, roomWidth, roomHeight))
                {
                    AddRoom(startX, startY, roomWidth, roomHeight);
                    roomAdded = true; 
                }
            }
        }
        return this;
    }

    public MapBuilder AddTunnelBetweenPoints(Vector2Int start, Vector2Int end)
    {
        var current = start;
        while (current.x != end.x)
        {
            var direction = end.x > current.x ? 1 : -1;
            current.x += direction;
            _grid[current.x, current.y].IsWalkable = true; 
        }

        // Then tunnel vertically
        while (current.y != end.y)
        {
            var direction = end.y > current.y ? 1 : -1;
            current.y += direction;
            _grid[current.x, current.y].IsWalkable = true; 
        }

        return this;
    }


    public Tile[,] Build()
    {
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (_grid[x, y].IsWalkable)
                {
                    var position = new Vector3(x, 0, y);
                    Object.Instantiate(_floorPrefab, position, Quaternion.identity, _parentTransform);
                }
            }
        }

        return _grid;
    }
    
    public List<Room> GetRooms()
    {
        return _rooms;
    } 
}