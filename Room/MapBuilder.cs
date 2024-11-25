using UnityEngine;
using System.Collections.Generic;

public class MapBuilder
{
    private Tile[,] _grid;
    private List<Room> _rooms;
    private int _width, _height;
    private GameObject _floorPrefab;
    private Material _tunnelMaterial;
    private Transform _parentTransform;

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
                _grid[x, y] = new Tile(x, y, false, false);
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

        const int minDistance = 5;
        for (var x = startX - minDistance; x < startX + roomWidth + minDistance; x++)
        {
            for (var y = startY - minDistance; y < startY + roomHeight + minDistance; y++)
            {
                if (x < 0 || y < 0 || x >= _width || y >= _height)
                {
                    continue;
                }

                if (_grid[x, y].IsRoom)
                {
                    return false;
                }
            }
        }

        return true;
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
                _grid[x, y].IsRoom = true;
            }
        }

        
        _rooms.Add(room); 
        return this;
    }

    public MapBuilder AddRandomRooms(int roomCount, int minWidth, int minHeight, int maxWidth, int maxHeight)
    {
        _rooms = new List<Room>();
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
            _grid[current.x, current.y].IsRoom = false; 
            _grid[current.x, current.y].IsTunnelPath = true; 
        }

        // Then tunnel vertically
        while (current.y != end.y)
        {
            var direction = end.y > current.y ? 1 : -1;
            current.y += direction;
            _grid[current.x, current.y].IsRoom = false; 
            _grid[current.x, current.y].IsTunnelPath = true; 
        }

        return this;
    }

    public MapBuilder AddTunnel(List<(Vector2Int, Vector2Int)> mstEdges)
    {
        foreach (var edge in mstEdges)
        {
            AddTunnelBetweenPoints(edge.Item1, edge.Item2);
        }

        return this;
    }
    
    public Tile[,] Build()
    {
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (_grid[x, y].IsRoom || _grid[x,y].IsTunnelPath)
                {
                    var position = new Vector3(x*2, 0, y*2);
                    var tileObject = Object.Instantiate(_floorPrefab, position, Quaternion.identity, _parentTransform);
                    
                    SetTileAttribute(tileObject);
                    _grid[x, y].TileObject = tileObject;
                }
            }
        }

        return _grid;
    }

    private void SetTileAttribute(GameObject tileObject)
    {
        tileObject.tag = "Tile";
        tileObject.AddComponent<BoxCollider>();
        tileObject.AddComponent<Renderer>();
    }
    
    public List<Vector2Int> GetAllRoomCenters(List<Room> rooms)
    {
        var roomCenters = new List<Vector2Int>();
        foreach (var room in _rooms)
        {
            var center = room.GetCenter();
            roomCenters.Add(center);
        }

        return roomCenters;
    }
    
    public List<Room> GetRooms()
    {
        return _rooms;
    } 
}