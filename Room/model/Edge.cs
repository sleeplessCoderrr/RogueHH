using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public int RoomA, RoomB;
    public float Weight;

    public Edge(int roomA, int roomB, float weight)
    {
        RoomA = roomA;
        RoomB = roomB;
        Weight = weight;
    }
}
