using System.Collections.Generic;
using UnityEngine;

public static class MoveUtility
{
    public static Vector3 GetPlayerData()
    {
        return PlayerDirector.Instance.playerData.playerPosition;
    }
    
    public static List<Vector2Int> GetPath(Tile[,] tileMap, Vector2Int start, Vector2Int end)
    {
        return AStarPathfinder.FindPath(tileMap, start, end);
    }
    
    public static GameObject GetTileFromRaycast(Camera camera)
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            return hit.collider?.gameObject;
        }
        return null;
    }
    
    public static bool ArePathEqual(List<Vector2Int> path1, List<Vector2Int> path2)
    {
        if (path1.Count != path2.Count) return false;
        for (var i = 0; i < path1.Count; i++)
        {
            if (path1[i] != path2[i]) return false;
        }

        return true;
    }
}