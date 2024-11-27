using UnityEngine;

public class RoomDecorationBuilder : BaseMapBuilder
{
    public override void InitializeGrid()
    {
        Grid = MapManager.Instance.mapData.MapTileData;
    }
}