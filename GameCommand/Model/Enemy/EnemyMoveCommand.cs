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

    public EnemyMoveCommand(Enemy enemy, GameObject instance, List<Vector2Int> path, EnemyStateManager stateManager)
    {
        _path = path;
        _enemy = enemy;
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
        Vector2Int firstTile;
        
        if (_path.Count > 0)
        {
            firstTile = _path[1];
        }
        else
        {
            firstTile = _path[0];
        }
        var targetPosition = new Vector3(firstTile.x * 2, _enemyInstance.transform.position.y, firstTile.y * 2);
        yield return CoroutineManager.Instance.StartCoroutine(MoveToTarget(targetPosition));

        _enemyStateManager.SetState(EnemyState.Aggro);
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        float moveSpeed = 2f; 
        float rotationSpeed = 10f; // Speed for rotating the enemy
        float distance;

        do
        {
            distance = Vector3.Distance(_enemyInstance.transform.position, targetPosition);
            var direction = (targetPosition - _enemyInstance.transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _enemyInstance.transform.rotation = Quaternion.Slerp(
                    _enemyInstance.transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            _enemyInstance.transform.position = Vector3.MoveTowards(
                _enemyInstance.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            yield return null; 
        }
        while (distance > 0.1f); 
    }

}
