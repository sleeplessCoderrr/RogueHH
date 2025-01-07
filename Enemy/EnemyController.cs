using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Tile[,] _tiles;
    private bool _isActive;
    private Vector3 _playerPosition;
    private const float AlertRange = 10f;
    private EnemyStateManager _stateManager;
    private CommandInvoker _commandInvoker;

    public EnemyData enemyData;
    public InfoDisplay infoDisplay;

    private void Start()
    {
        _isActive = false;
        enemyData = ScriptableObject.CreateInstance<EnemyData>();
        _tiles = MapManager.Instance.mapData.MapTileData;
        _commandInvoker = InputManager.Instance.CommandInvoker;
        _stateManager = GetComponent<EnemyStateManager>();
        SetAttribute();
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

    private void SetAttribute()
    {
        var attributes = new EnemyAttributes(
            enemyData.currentHealth,
            enemyData.attack,
            enemyData.defense
        );

        var scaledAttributes = EnemyAttributeScaler.ScaleAttributes(
            attributes,
            PlayerDirector.Instance.playerData.selectedLevel
        );

        enemyData.maxHealth = enemyData.currentHealth = scaledAttributes.Health;
        enemyData.attack = scaledAttributes.Attack;
        enemyData.defense = scaledAttributes.Defense;
    }

    private void CheckLineOfSight()
    {
        if (!_isActive || PlayerDirector.Instance.playerData.isPlayerTurn ) return;

        enemyData.isTurn = true;
        _commandInvoker.AddCommand(new CheckLOSCommand(gameObject));
        _commandInvoker.ExecuteCommand();
        _isActive = false;
    }

    private void CheckStats()
    {
        infoDisplay.UpdateInfo(
            enemyData.currentHealth, 
            enemyData.maxHealth
        );
    }
}