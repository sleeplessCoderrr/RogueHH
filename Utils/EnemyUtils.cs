using System;
using UnityEngine;

public static class EnemyUtils
{
    public static bool CheckPlayerRange(Transform transform, Vector3 playerPosition, float alertRange)
    {
        return Vector3.Distance(transform.position, playerPosition) <= alertRange;
    }

    public static bool CheckPlayerPosition(Vector2Int nextPosition)
    {
        var playerPosition = PlayerDirector.Instance.Player.PlayerInstance.transform.position;
        var targetPosition = new Vector3(nextPosition.x * 2, PlayerDirector.Instance.Player.PlayerInstance.transform.position.y, nextPosition.y * 2);
        Debug.Log(playerPosition);
        Debug.Log(targetPosition);
        
        return playerPosition == targetPosition;
    }
}