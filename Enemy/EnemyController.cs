﻿using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    private bool _oneTurn;
    private bool _isAlert;
    private bool _isActive;
        
    private Tile[,] _tiles;
    private Vector3 _playerPosition;
    private EnemyState _currentState;
    private const float AlertRange = 10f;
    private CommandInvoker _commandInvoker;
    private EnemyStateManager _stateManager;

    public EnemyData enemyData;
    public StateText currentText;
    public InfoDisplay infoDisplay;

    private void Start()
    {
        _oneTurn = false;
        _isAlert = false;
        _isActive = false;
        
        enemyData = ScriptableObject.CreateInstance<EnemyData>();
        _tiles = MapManager.Instance.mapData.MapTileData;
        _commandInvoker = InputManager.Instance.CommandInvoker;
        _stateManager = gameObject.GetComponent<EnemyStateManager>();
        _currentState = _stateManager.currentState;
        SetAttribute();
    }

    private void Update()
    {
        _playerPosition = PlayerDirector
            .Instance
            .playerData
            .playerPosition;
        
        CheckStats();
    }

    public void ExecuteTurn()
    {
        // if (_oneTurn) return;
        CheckPlayerPosition();
        CheckLineOfSight();
    }

    private void CheckPlayerPosition()
    {
        if (EnemyUtils.CheckPlayerRange(transform, _playerPosition, AlertRange))
        {
            _isActive = true;
            if (_isAlert) return;
            
            _stateManager.SetState(EnemyState.Alert);
            _currentState = EnemyState.Alert;
            currentText.UpdateIndicator(EnemyState.Alert);
            _isAlert = true;
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
        if (gameObject.GetComponent<EnemyLineOfSight>().playerInSight)
        { 
            _stateManager.SetState(EnemyState.Aggro);
            currentText.UpdateIndicator(EnemyState.Aggro);
            
            var lookAtEnemy = gameObject.AddComponent<LookAtPlayer>();
            lookAtEnemy.player = PlayerDirector.Instance.Player.PlayerInstance.transform;
            lookAtEnemy.isActive = true;
        }

        Debug.Log("Ga keliatan");
        _isActive = false;
        // _oneTurn = true;
        PlayerDirector.Instance.playerData.isPlayerTurn = true;
    }

    private void CheckStats()
    {
        infoDisplay.UpdateInfo(
            enemyData.currentHealth, 
            enemyData.maxHealth
        );
    }
}