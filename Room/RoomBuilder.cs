using UnityEngine;
using System.Collections.Generic;

public class RoomBuilder : BaseMapBuilder
{
    private List<Room> _rooms;

    public RoomBuilder SetDimensions(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        return this;
    }

    public override void InitializeGrid()
    {
        Grid = new Tile[Width, Height];
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                Grid[x, y] = new Tile(x, y, false, false, false);
            }
        }
    }
    
    private RoomBuilder AddRoom(int startX, int startY, int roomWidth, int roomHeight)
    {
        var room = new Room(startX, startY, roomWidth, roomHeight);
        for (var x = startX; x < startX + roomWidth; x++)
        {
            for (var y = startY; y < startY + roomHeight; y++)
            {
                Grid[x, y].IsRoom = true;
            }
        }

        
        _rooms.Add(room); 
        return this;
    }

    public RoomBuilder AddRandomRooms(int roomCount, int minWidth, int minHeight, int maxWidth, int maxHeight)
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
                var startX = rand.Next(0, Width - roomWidth);
                var startY = rand.Next(0, Height - roomHeight);

                if (MapUtility.IsValidRoomPosition(Grid , Width, Height, startX, startY, roomWidth, roomHeight))
                {
                    AddRoom(startX, startY, roomWidth, roomHeight);
                    roomAdded = true;
                }
            }
        }

        return this;
    }

    public List<Room> GetRooms()
    {
        return _rooms;
    } 
}