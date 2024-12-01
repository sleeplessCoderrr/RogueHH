using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCommand : ICommand
{
    private readonly List<Vector2Int> _path;
    private readonly PlayerData _playerData;
    private readonly PlayerConfig _playerConfig;
    

    public PlayerMoveCommand(List<Vector2Int> path)
    {
        _path = path;
        _playerData = PlayerDirector.Instance.playerData;
        _playerConfig = PlayerDirector.Instance.playerConfig;
    }

    public void Execute()
    {
        CoroutineManager.Instance.StartManagedCoroutine(MovePlayer());
    }

    private IEnumerator MovePlayer()
    {
        var player = PlayerDirector.Instance.playerInstance;
        foreach (var pathPoint in _path)
        {
            var targetPosition = new Vector3(
                pathPoint.x * 2, 
                player.transform.position.y, 
                pathPoint.y * 2
            );
            while (Vector3.Distance(player.transform.position, targetPosition) > 0.01f)
            {
                player.transform.position = Vector3.MoveTowards(
                    player.transform.position, 
                    targetPosition, 
                    Time.deltaTime * _playerConfig.walkSpeed
                );
                player.transform.LookAt(player.transform.position + player.transform.forward);
                _playerData.playerPosition = new Vector3(
                    player.transform.position.x, 
                    player.transform.position.y, 
                    player.transform.position.z
                );
                yield return null;
            }
        }
    }
}