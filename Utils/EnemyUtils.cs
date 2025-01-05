using System;
using UnityEngine;

public static class EnemyUtils
{
    public static bool CheckPlayerRange(int range, Transform pos, Vector3 playerPos)
    {
        var distance = Math.Abs(pos.position.x / 2 - playerPos.x / 2)
                       + Math.Abs(pos.position.z / 2 - playerPos.z / 2);
        
        return distance <= range;
    }
}