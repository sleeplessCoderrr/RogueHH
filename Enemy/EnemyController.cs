using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Tile[,] _tiles;
    private Vector3 _playerPosition;
    private bool _isActive;

    private EnemyStateManager _stateManager;
    private float _alertRange = 8f;

    private void Start()
    {
        _isActive = false;
        _tiles = MapManager.Instance.mapData.MapTileData;
        _stateManager = GetComponent<EnemyStateManager>();
    }

    private void Update()
    {
        _playerPosition = PlayerDirector.Instance.playerData.playerPosition;

        CheckPlayer();
        CheckLineOfSight();
    }

    private void CheckPlayer()
    {
        var distance = Vector3.Distance(transform.position, _playerPosition);
        if (distance <= _alertRange)
        {
            if (!_isActive)
            {
                _isActive = true;
                _stateManager.SetState(EnemyState.Alert);
            }
        }
        else
        {
            if (_isActive)
            {
                _isActive = false;
                _stateManager.SetState(EnemyState.Idle);
            }
        }
    }

    private void CheckLineOfSight()
    {
        if (!_isActive) return;

        // if (EnemyUtils.HasLineOfSight(transform, _playerPosition, _stateManager._enemy.LOSBlockingLayers))
        // {
        //     _stateManager.SetState(EnemyState.Aggro);
        // }
    }
}