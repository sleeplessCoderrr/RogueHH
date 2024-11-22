using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "SO/MapConfig")]
public class MapConfig : ScriptableObject
{
    [Header("Map Prefabs")]
    public GameObject floorTile;
    public GameObject[] floorDecorations;
    public GameObject[] roomDecorations;
    [Header("Map Settings")]
    public int width;
    public int height;
    public int minWidth;
    public int minHeight;
    public int maxWidth;
    public int maxHeight;
}
