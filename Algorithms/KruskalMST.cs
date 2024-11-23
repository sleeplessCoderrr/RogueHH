using System.Collections.Generic;
using UnityEngine;

public class KruskalMST
{
    private Dictionary<Vector2Int, Vector2Int> _parent;

    public KruskalMST()
    {
        _parent = new Dictionary<Vector2Int, Vector2Int>();
    }

    public List<(Vector2Int, Vector2Int)> Compute(List<Vector2Int> roomCenters)
    {
        var mstEdges = new List<(Vector2Int, Vector2Int)>();
        var edges = new List<(float, Vector2Int, Vector2Int)>();

        for (var i = 0; i < roomCenters.Count; i++)
        {
            for (var j = i + 1; j < roomCenters.Count; j++)
            {
                var distance = Vector2Int.Distance(roomCenters[i], roomCenters[j]);
                edges.Add((distance, roomCenters[i], roomCenters[j]));
            }
        }

        edges.Sort((a, b) => a.Item1.CompareTo(b.Item1));
        foreach (var room in roomCenters)
        {
            _parent[room] = room; // Each room starts as its own parent
        }

        foreach (var (distance, room1, room2) in edges)
        {
            if (Find(room1) != Find(room2)) // Avoid cycles
            {
                mstEdges.Add((room1, room2));
                Union(room1, room2);
            }

            if (mstEdges.Count == roomCenters.Count - 1)
            {
                break;
            }
        }

        return mstEdges;
    }

    private Vector2Int Find(Vector2Int node)
    {
        if (_parent[node] != node)
        {
            _parent[node] = Find(_parent[node]);
        }
        return _parent[node];
    }

    private void Union(Vector2Int a, Vector2Int b)
    {
        var rootA = Find(a);
        var rootB = Find(b);
        if (rootA != rootB)
        {
            _parent[rootB] = rootA;
        }
    }
}
