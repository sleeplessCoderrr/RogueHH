using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCommand : ICommand
{
    private readonly Player _player;
    private readonly List<Vector2Int> _path;
    private readonly GameObject _playerInstance;
    private readonly PlayerStateManager _playerStateManager;

    public PlayerMoveCommand(List<Vector2Int> path)
    {
        _path = path;
        _player = PlayerDirector.Instance.Player;
        _playerStateManager = PlayerStateManager.Instance;
        _playerInstance = _player.PlayerInstance;
    }

    public void Execute()
    {
        CoroutineManager.Instance.StartManagedCoroutine(MovePlayer());
    }

    private IEnumerator MovePlayer()
    {
        _playerStateManager.SetState(PlayerState.Walking);
        PlayerDirector.Instance.playerData.isPlayerTurn = true;
        foreach (var pathPoint in _path)
        {
            if (InputManager.Instance.isCanceled)
            {
                InputManager.Instance.isCanceled = false;
                PlayerDirector.Instance.playerData.isPlayerTurn = false;
                break;
            }
            var targetPosition = GetTargetPosition(pathPoint);
            _player.TargetPosition = targetPosition;
            _player.LookAtTarget(_playerInstance, targetPosition); 
            
            yield return 
                CoroutineManager.Instance.StartCoroutine(
                    _player.MoveToTarget(
                        _player, 
                        _playerInstance, 
                        targetPosition)
                );
            _player.UpdateData(_playerInstance); 
        }

        _playerStateManager.SetState(PlayerState.Idle);
        PlayerDirector.Instance.playerData.isPlayerTurn = false;
    }
    
    private Vector3 GetTargetPosition(Vector2Int pathPoint)
    {
        return new Vector3(
            pathPoint.x * 2,
            _playerInstance.transform.position.y,
            pathPoint.y * 2
        );
    }
}

