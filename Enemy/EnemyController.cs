using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Tile[,] _tiles;
    private bool _isActive;
    private Vector3 _playerPosition;
    private const float AlertRange = 8f;
    private EnemyStateManager _stateManager;

    public EnemyData enemyData;
    public InfoDisplay infoDisplay;

    private void Start()
    {
        _isActive = false;
        enemyData = ScriptableObject.CreateInstance<EnemyData>();
        _tiles = MapManager.Instance.mapData.MapTileData;
        _stateManager = GetComponent<EnemyStateManager>();
        
    }

    private void Update()
    {
        _playerPosition = PlayerDirector
        .Instance
        .playerData
        .playerPosition;
        CheckPlayer();
        CheckLineOfSight();
        CheckStats();
    }

    private void CheckPlayer()
    {
        var isInRange = EnemyUtils.CheckPlayerRange(transform, _playerPosition, AlertRange);
        if (isInRange != _isActive)
        {
            _isActive = isInRange;
            _stateManager.SetState(
                isInRange ? EnemyState.Alert : EnemyState.Idle
            );
        }
    }


    private void CheckLineOfSight()
    {
        if (!_isActive) return;

        // You can re-enable this when implementing Aggro state detection
        // if (EnemyUtils.HasLineOfSight(transform, _playerPosition, _stateManager._enemy.LOSBlockingLayers))
        // {
        //     _stateManager.SetState(EnemyState.Aggro);
        // }
    }

    private void CheckStats()
    {
        infoDisplay.UpdateInfo(
            enemyData.currentHealth, 
            enemyData.maxHealth
        );
    }
}