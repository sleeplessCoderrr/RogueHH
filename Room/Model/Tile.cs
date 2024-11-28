using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int X, Y;
    public GameObject TileObject;
    public bool IsFloorDecoration;
    public bool IsRoomDecoration;
    public bool IsTunnelPath;
    public bool IsBuffer;
    public bool IsRoom;

    public Tile(int x, int y, bool isRoom, bool isTunnelPath, bool isRoomDecoration, bool isFloorDecoration, bool isBuffer)
    {
        this.X = x;
        this.Y = y;
        this.IsRoom = isRoom;
        this.IsTunnelPath = isTunnelPath;
        this.IsRoomDecoration = isRoomDecoration;
        this.IsFloorDecoration = isFloorDecoration;
        this.IsBuffer = isBuffer;
    }
}


