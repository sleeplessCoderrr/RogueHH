using System.Collections.Generic;
using UnityEngine;

public static class AStarPathfinder
{
    public static List<Vector2Int> FindPath(Tile[,] grid, Vector2Int start, Vector2Int end)
    {
        var openSet = new PriorityQueue<Vector2Int>();
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        var gScore = new Dictionary<Vector2Int, float>();
        var fScore = new Dictionary<Vector2Int, float>();

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var pos = new Vector2Int(x, y);
                gScore[pos] = float.MaxValue;
                fScore[pos] = float.MaxValue;
            }
        }

        gScore[start] = 0;
        fScore[start] = Heuristic(start, end);

        openSet.Enqueue(start, fScore[start]);

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (current == end)
                return ReconstructPath(cameFrom, current);

            foreach (var neighbor in GetNeighbors(grid, current))
            {
                float tentativeGScore = gScore[current] + 1; 

                if (tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, end);

                    if (!openSet.Contains(neighbor))
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                }
            }
        }

        return new List<Vector2Int>();
    }

    private static List<Vector2Int> GetNeighbors(Tile[,] grid, Vector2Int current)
    {
        var neighbors = new List<Vector2Int>();
        var directions = new Vector2Int[]
        {
            new Vector2Int(0, 1), // Up
            new Vector2Int(0, -1), // Down
            new Vector2Int(1, 0), // Right
            new Vector2Int(-1, 0) // Left
        };

        foreach (var dir in directions)
        {
            var neighbor = current + dir;

            if (IsValid(grid, neighbor))
                neighbors.Add(neighbor);
        }

        return neighbors;
    }

    private static bool IsValid(Tile[,] grid, Vector2Int pos)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        return pos.x >= 0 && pos.y >= 0 && pos.x < width && pos.y < height && grid[pos.x, pos.y].IsRoom;
    }

    private static float Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private static List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        var path = new List<Vector2Int> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}

public class PriorityQueue<T>
{
    private List<(T item, float priority)> _elements = new List<(T, float)>();

    public int Count => _elements.Count;

    public void Enqueue(T item, float priority)
    {
        _elements.Add((item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 1; i < _elements.Count; i++)
        {
            if (_elements[i].priority < _elements[bestIndex].priority)
                bestIndex = i;
        }

        var bestItem = _elements[bestIndex];
        _elements.RemoveAt(bestIndex);
        return bestItem.item;
    }

    public bool Contains(T item)
    {
        foreach (var element in _elements)
        {
            if (EqualityComparer<T>.Default.Equals(element.item, item))
                return true;
        }
        return false;
    }
}
