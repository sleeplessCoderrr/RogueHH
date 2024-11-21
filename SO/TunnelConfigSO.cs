using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TunnelConfig", menuName = "Map/TunnelConfig")]
public class TunnelConfigSO : ScriptableObject
{   
    [Header("Settings")]
    public float tunnelWidth = 1f;
}
