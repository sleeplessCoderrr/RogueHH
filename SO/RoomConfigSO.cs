using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "Map/RoomConfig")]
public class RoomConfigSO : ScriptableObject
{   
    [Header("Settings")]
    public int widthX = 10;
    public int widthY = 10;
    public float spacing = 2f;
    public float objectYOffset = 1f;
}
