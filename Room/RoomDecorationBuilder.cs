using System.Collections.Generic;
using Builder;
using UnityEngine;

public class RoomDecorationBuilder : MapBaseBuilder
{
    public override void InitializeGrid()
    {
        SetDimensions();
        Grid = MapManager.Instance.mapData.MapTileData;
        Rooms = MapManager.Instance.mapData.Rooms;
    }
    
    private void SetDimensions()
    {
        Height = MapManager.Instance.mapConfig.height;
        Width = MapManager.Instance.mapConfig.width;
    }

    public RoomDecorationBuilder AddRoomDecoration(List<Room> rooms)
    {
        foreach (var room in rooms)
        {
            MakeRoomDecoration(room);
        }
        return this;
    }

    private static Vector3Int HeightBaseOnPrefabs(int index, int x, int y)
    {
        return index == 0 ? new Vector3Int(x*2, -1, y*2) : new Vector3Int(x*2, 1, y*2);
    }

    private void MakeRoomDecoration(Room room)
    {
        var roomSize = room.Width * room.Height;
        var thirtyPercent = (roomSize * 8) / 100;

        var decorCount = 0;
        while (decorCount < thirtyPercent)
        {
            var x = Random.Range(room.X, room.X + room.Width);
            var y = Random.Range(room.Y, room.Y + room.Height);
            if (MapUtility.IsValidDecoration(Grid, x, y)
                && MapUtility.IsNoNeighbourDecoration(Grid, x, y)
                && !MapUtility.IsValidFloorDecoration(Grid, x, y)
                && !MapUtility.IsValidRoomDecoration(Grid, x, y)
                && MapUtility.DecorationChance())
            {
                Grid[x, y].IsRoomDecoration = true;
                Grid[x, y].IsRoom = false;
                decorCount++;

                var randomIndex = MapUtility.TakeRandomPrefabs(Prefabs);
                var randomRotation = MapUtility.GetRandomRotation();
                var position = HeightBaseOnPrefabs(randomIndex, x, y);
                var tileObject = Object.Instantiate(
                    Prefabs[randomIndex], 
                    position, 
                    Quaternion.Euler(0, randomRotation, 0), 
                    ParentTransform
                );
            }
        }
    }
}