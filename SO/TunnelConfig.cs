using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TunnelConfig", menuName = "Map/TunnelConfig")]
public class TunnelConfig : ScriptableObject
{   
    [Header("Prefabs Assets")]
    public GameObject floorTile;
    public GameObject[] floorDecorations;
    [Header("Settings")]
    public float tunnelWidth = 1f;
}
