using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "SO/Data/MapData")]
public class MapData : ScriptableObject
{
    public Tile[,] MapTileData;
    public List<Room> Rooms;
    public List<Vector2Int> roomCenters;
    public List<(Vector2Int, Vector2Int)> MstEdges;
}