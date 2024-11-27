using UnityEngine;

public class FloorDecorationBuilder : BaseMapBuilder
{
    public override void InitializeGrid()
    {
        Grid = MapManager.Instance.mapData.MapTileData;
    }
}