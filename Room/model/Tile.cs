using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 Position {get; set;}
    public bool IsWalkable {get; set;}
    public List<Tile> Neighbors {get; set;}

    public Tile(Vector3 pos, bool isWalkable)
    {
        Position = pos;
        IsWalkable = isWalkable;
    }

    public void AddNeighbor(Tile neighbor)
    {
        Neighbors.Add(neighbor);
    }
}
