using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "Map/RoomConfig")]
public class RoomConfig : ScriptableObject
{   
    [Header("Prefabs Assets")]
    public GameObject floorTile;
    public GameObject[] floorDecorations;
    public GameObject[] roomDecorations;
    [Header("Settings")]
    public int widthX = 10;
    public int widthY = 10;
    public float spacing = 1f;
    public float objectYOffset = 2f;
}
