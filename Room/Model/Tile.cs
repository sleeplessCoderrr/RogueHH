using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int X, Y;
    public GameObject TileObject;
    public bool IsTunnelPath;
    public bool IsDecoration;
    public bool IsRoom;

    public Tile(int x, int y, bool isRoom, bool isTunnelPath, bool isDecoration)
    {
        this.X = x;
        this.Y = y;
        this.IsRoom = isRoom;
        this.IsTunnelPath = isTunnelPath;
        this.IsDecoration = isDecoration;
    }
}


