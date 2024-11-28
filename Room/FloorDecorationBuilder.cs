using System.Collections.Generic;
using UnityEngine;

public class FloorDecorationBuilder : BaseMapBuilder
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

    public FloorDecorationBuilder AddFloorDecoration(List<Room> rooms)
    {
        foreach (Room room in rooms)
        {
            MakeFloorDecoration(room);
            Debug.Log(room.X + "," + room.Y + "," + room.Width + "," + room.Height);
        }

        return this;
    }
    
    private void MakeFloorDecoration(Room room)
    {
        for (var x = room.X; x < room.X + room.Width; x++)
        {
            for (var y = room.Y; y < room.Y + room.Height; y++)
            {
                if (MapUtility.IsValidRoom(Grid, x, y) 
                    && !MapUtility.IsValidPath(Grid, x, y)
                    && MapUtility.FloorDecorationChance())
                {
                    var position = new Vector3(x*2, 1, y*2);
                    var tileObject = Object.Instantiate(
                        MapUtility.TakeRandomFloor(Prefabs), 
                        position, 
                        Quaternion.identity, 
                        ParentTransform);
                }
            }
        }
    }
}