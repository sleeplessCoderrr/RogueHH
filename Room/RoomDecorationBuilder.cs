using System.Collections.Generic;
using UnityEngine;

public class RoomDecorationBuilder : BaseBuilder
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

    private void MakeRoomDecoration(Room room)
    {
        var roomSize = room.Width * room.Height;
        var thirtyPercent = (roomSize * 5) / 100;

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
                Grid[x,y].IsRoomDecoration = true;
                decorCount++;

                var randomIndex = MapUtility.TakeRandomPrefabs(Prefabs);
                var position = new Vector3(x*2, 1, y*2);
                var tileObject = Object.Instantiate(
                    Prefabs[randomIndex], 
                    position, 
                    Quaternion.identity, 
                    ParentTransform
                );
            }
        }
    }
}