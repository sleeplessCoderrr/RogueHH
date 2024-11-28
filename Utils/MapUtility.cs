using UnityEngine;
using System.Collections.Generic;

public static class MapUtility
{
    public static bool IsValidRoomPosition(Tile[,] grid, int width, int height, int startX, int startY, int roomWidth, int roomHeight)
    {
        if (startX < 0 || startY < 0 || startX + roomWidth > width || startY + roomHeight > height)
        {
            return false;
        }

        const int minDistance = 5;
        for (var x = startX - minDistance; x < startX + roomWidth + minDistance; x++)
        {
            for (var y = startY - minDistance; y < startY + roomHeight + minDistance; y++)
            {
                if (x < 0 || y < 0 || x >= width || y >= height)
                {
                    continue;
                }

                if (grid[x, y].IsRoom)
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    public static List<Vector2Int> GetAllRoomCenters(List<Room> rooms)
    {
        var roomCenters = new List<Vector2Int>();
        foreach (var room in rooms)
        {
            var center = room.GetCenter();
            roomCenters.Add(center);
        }

        return roomCenters;
    }

    public static bool IsValidDecoration(Tile[,] grid, int x, int y)
    {
        return (x <= 0 || !grid[x - 1, y].IsTunnelPath) && // Left
               (x >= grid.GetLength(0) - 1 || !grid[x + 1, y].IsTunnelPath) && // Right
               (y <= 0 || !grid[x, y - 1].IsTunnelPath) && // Bottom
               (y >= grid.GetLength(1) - 1 || !grid[x, y + 1].IsTunnelPath); // Top
    }

    public static bool IsValidRoom(Tile[,] tiles, int x, int y)
    {
        return tiles[x, y].IsRoom;
    }

    public static bool IsValidPath(Tile[,] tiles, int x, int y)
    {
        return tiles[x, y].IsTunnelPath;
    }

    public static bool IsValidFloorDecoration(Tile[,] tiles, int x, int y)
    {
        return tiles[x, y].IsFloorDecoration;
    }

    public static bool IsValidRoomDecoration(Tile[,] tiles, int x, int y)
    {
        return tiles[x, y].IsRoomDecoration;
    }


    public static int TakeRandomPrefabs(GameObject[] prefabs)
    {
        return Random.Range(0, prefabs.Length);
    }
    
    public static void SetTileAttribute(GameObject tileObject)
    {
        tileObject.tag = "Tile";
        tileObject.AddComponent<BoxCollider>();
    }
    
    public static bool FloorDecorationChance()
    {
        return UnityEngine.Random.Range(0f, 100f) >= 95;
    }
    
    public static bool DecorationChance()
    {
        return UnityEngine.Random.Range(0f, 100f) >= 98;
    }
}