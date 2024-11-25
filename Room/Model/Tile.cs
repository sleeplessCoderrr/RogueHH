using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int X, Y;
    public GameObject TileObject;
    public bool IsRoom;
    public bool IsTunnelPath;

    public Tile(int x, int y, bool isRoom, bool isTunnelPath)
    {
        this.X = x;
        this.Y = y;
        this.IsRoom = isRoom;
        this.IsTunnelPath = isTunnelPath;
    }
}


