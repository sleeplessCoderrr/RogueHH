using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GridManager gridManager;

    public List<Node> FindPath(Vector2Int start, Vector2Int target)
    {
        Node startNode = gridManager.grid[start.x, start.y];
        Node targetNode = gridManager.grid[target.x, target.y];

        if (!targetNode.isWalkable) return null;

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            foreach (Node node in openList)
            {
                if (node.fCost < currentNode.fCost || (node.fCost == currentNode.fCost && node.hCost < currentNode.hCost))
                {
                    currentNode = node;
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.isWalkable || closedList.Contains(neighbor)) continue;

                float newMovementCost = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCost < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCost;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null; // No path found
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int checkPos = node.position + dir;
            if (checkPos.x >= 0 && checkPos.x < gridManager.mapWidth && checkPos.y >= 0 && checkPos.y < gridManager.mapHeight)
            {
                neighbors.Add(gridManager.grid[checkPos.x, checkPos.y]);
            }
        }

        return neighbors;
    }

    float GetDistance(Node a, Node b)
    {
        int dx = Mathf.Abs(a.position.x - b.position.x);
        int dy = Mathf.Abs(a.position.y - b.position.y);
        return dx + dy;
    }
}

