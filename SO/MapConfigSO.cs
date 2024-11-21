using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "Map/MapConfig")]
public class MapConfigSO : ScriptableObject
{
    [Header("Prefabs Assets")]
    public GameObject floorTile;
    public GameObject[] floorDecorations;
    public GameObject[] roomDecorations;
    [Header("Map Size")]
    public int widthX = 10;
    public int widthY = 10;
}