﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCommand : ICommand
{
    private readonly Player _player;
    private readonly List<Vector2Int> _path;
    private readonly GameObject _playerInstance;
    private readonly PlayerStateManager _playerStateManager;
    
    public CommandType CommandType { get; set; }

    public PlayerMoveCommand(List<Vector2Int> path)
    {
        _path = path;
        CommandType = CommandType.Player;
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
        if (InputManager.Instance.isEnemyNearby)
        {
            _playerStateManager.SetState(PlayerState.Walking);
            PlayerDirector.Instance.playerData.isPlayerTurn = true;

            if (_path.Count > 0)
            {
                var targetPosition = GetTargetPosition(_path[0]);
                _player.TargetPosition = targetPosition;
                _player.LookAtTarget(_playerInstance, targetPosition);

                yield return CoroutineManager.Instance.StartCoroutine(
                    _player.MoveToTarget(
                        _player,
                        _playerInstance,
                        targetPosition)
                );
                _player.UpdateData(_playerInstance);
            }

            _playerStateManager.SetState(PlayerState.Idle);
            PlayerDirector.Instance.playerData.isPlayerTurn = false;
            yield break;
        }

        _playerStateManager.SetState(PlayerState.Walking);
        PlayerDirector.Instance.playerData.isPlayerTurn = true;

        foreach (var pathPoint in _path)
        {
            
            
            if (MapUtility.IsEnemyInRange(
                _playerInstance.transform.position,
                10f,
                EnemyDirector.Instance.EnemyList))
            {
                InputManager.Instance.isCanceled = false;
                PlayerDirector.Instance.playerData.isPlayerTurn = false;
                break;
            }
            
            if (InputManager.Instance.isCanceled)
            {
                InputManager.Instance.isCanceled = false;
                PlayerDirector.Instance.playerData.isPlayerTurn = false;
                break;
            }
            
            var targetPosition = GetTargetPosition(pathPoint);
            _player.TargetPosition = targetPosition;
            _player.LookAtTarget(_playerInstance, targetPosition);

            yield return CoroutineManager.Instance.StartCoroutine(
                _player.MoveToTarget(
                    _player,
                    _playerInstance,
                    targetPosition)
            );
            _player.UpdateData(_playerInstance);
        }

        _playerStateManager.SetState(PlayerState.Idle);
        PlayerDirector.Instance.playerData.isPlayerTurn = false;

        if (InputManager.Instance.isLifeSteel)
        {
            InputManager.Instance.lifeSteelCount--;
            if (InputManager.Instance.lifeSteelCount == 0) InputManager.Instance.isLifeSteel = false;
        }
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

