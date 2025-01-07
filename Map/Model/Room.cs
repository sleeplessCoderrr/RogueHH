using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int X, Y, Width, Height;

    public Room(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    // Get the center of the room
    public Vector2Int GetCenter()
    {
        return new Vector2Int(X + Width / 2, Y + Height / 2);
    }
}
