using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int X, Y;
    public bool IsWalkable;

    public Tile(int x, int y, bool isWalkable)
    {
        this.X = x;
        this.Y = y;
        this.IsWalkable = isWalkable;
    }
}


