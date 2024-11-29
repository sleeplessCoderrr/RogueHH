using System.Collections.Generic;
using UnityEngine;

public abstract class EntitiesBuilder
{
    protected Transform ParentTransform;
    
    public abstract GameObject[] Build(MapConfig config, MapData mapData, int count);
    public void SetParent(Transform parent)
    {
        this.ParentTransform = parent;
    }
    
    protected static bool IsValidPosition(MapData mapData, int x, int y)
    {
        return !MapUtility.IsValidPath(mapData.MapTileData, x, y) 
               && MapUtility.IsValidRoom(mapData.MapTileData, x, y)
               && !MapUtility.IsValidRoomDecoration(mapData.MapTileData, x, y);
    }
}
