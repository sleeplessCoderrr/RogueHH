using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    public Vector3 Position {get; set;}
    public bool IsWalkable {get; set;}
    public float cost {get; set;}

    public Tile(Vector3 pos, bool isWalkable = true)
    {
        this.Position = pos;
        this.IsWalkable = isWalkable;
        this.cost = 1;
    }
}

