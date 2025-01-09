using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveCommand : ICommand
{
    private readonly Enemy _enemy;
    private GameObject _enemyInstance;
    private readonly List<Vector2Int> _path;
    private EnemyStateManager _enemyStateManager;
    
    public CommandType CommandType { get; set; }

    public EnemyMoveCommand(GameObject instance, List<Vector2Int> path, EnemyStateManager stateManager)
    {
        _path = path;
        _enemyInstance = instance;
        CommandType = CommandType.Enemy;
        _enemyStateManager = stateManager;
    }

    public void Execute()
    {
        CoroutineManager.Instance.StartManagedCoroutine(MoveEnemy());
    }

    private IEnumerator MoveEnemy()
    {
        _enemyStateManager.SetState(EnemyState.Walk);
        if (_path.Count > 0)
        {
            var targetPosition = GetTargetPosition(_path[0]);
            yield return CoroutineManager.Instance.StartCoroutine(
                _enemy.MoveToTarget(
                targetPosition
            ));
        }
        
        _enemyStateManager.SetState(EnemyState.Aggro);
        yield break;
    }
    
    private Vector3 GetTargetPosition(Vector2Int pathPoint)
    {
        return new Vector3(
            pathPoint.x * 2,
            _enemyInstance.transform.position.y,
            pathPoint.y * 2
        );
    }
}