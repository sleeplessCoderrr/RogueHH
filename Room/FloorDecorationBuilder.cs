using System.Collections.Generic;
using Builder;
using UnityEngine;

public class FloorDecorationBuilder : MapBaseBuilder
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
    
    public FloorDecorationBuilder AddFloorDecoration()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                if (MapUtility.IsValidRoom(Grid, x, y) 
                    && MapUtility.FloorDecorationChance())
                {
                    Grid[x,y].IsFloorDecoration = true;
                    
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
        return this;
    }
}