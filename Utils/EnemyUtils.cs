using System;
using UnityEngine;

public static class EnemyUtils
{
    public static bool CheckPlayerRange(Transform transform, Vector3 playerPosition, float alertRange)
    {
        return Vector3.Distance(transform.position, playerPosition) <= alertRange;
    }
}