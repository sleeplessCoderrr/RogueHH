﻿using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    private bool _isDoneTurn;
    public bool isAlreadyAlert;
    private bool _isContinueLOS;
        
    private Tile[,] _tiles;
    private Vector3 _playerPosition;
    private EnemyState _currentState;
    private const float AlertRange = 10f;
    private List<Vector2Int> _currentPath;
    private CommandInvoker _commandInvoker;
    private EnemyStateManager _stateManager;

    public EnemyData enemyData;
    public StateText currentText;
    public InfoDisplay infoDisplay;

    private void Start()
    {
        _isDoneTurn = false;
        _isContinueLOS = false;
        isAlreadyAlert = false;
        
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

        ExecuteTurn();
        CheckStats();
    }

    private void ExecuteTurn()
    {
        // if (enemyData.isTurn) return;
        CheckPlayerPosition();
        if (_currentState == EnemyState.Idle) return;
        CheckLineOfSight();
    }

    private void CheckPlayerPosition()
    {
        if (EnemyUtils.CheckPlayerRange(transform, _playerPosition, AlertRange))
        {
            _isContinueLOS = true;
            if (isAlreadyAlert) return;
            
            _stateManager.SetState(EnemyState.Alert);
            _currentState = EnemyState.Alert;
            currentText.UpdateIndicator(EnemyState.Alert);
            isAlreadyAlert = true;
        }
        else
        {
            _stateManager.SetState(EnemyState.Idle);
            _currentState = EnemyState.Idle;
            currentText.UpdateIndicator(EnemyState.Idle);
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
        if (!_isContinueLOS || PlayerDirector.Instance.playerData.isPlayerTurn ) return;
        enemyData.isTurn = true;
        
        _commandInvoker.AddCommand(new CheckLOSCommand(gameObject));
        _commandInvoker.ExecuteCommand();
        if (gameObject.GetComponent<EnemyLineOfSight>().playerInSight)
        { 
            _stateManager.SetState(EnemyState.Aggro);
            currentText.UpdateIndicator(EnemyState.Aggro);
            
            LookPlayer();
            Debug.Log("Jarak: " + Vector3.Distance(gameObject.transform.position, _playerPosition));
            if (Vector3.Distance(gameObject.transform.position, _playerPosition) <= 3f)
            {
                Debug.Log("In");
                AttackPlayer();
            }
            else
            {
                Move();
            }
        }
        else if(!gameObject.GetComponent<EnemyLineOfSight>().playerInSight)
        {
            _stateManager.SetState(EnemyState.Alert);
            _currentState = EnemyState.Alert;
            currentText.UpdateIndicator(EnemyState.Alert);
            isAlreadyAlert = false;
        }
        else
        {
            _stateManager.SetState(EnemyState.Idle);
            _currentState = EnemyState.Idle;
            currentText.UpdateIndicator(EnemyState.Idle);
            isAlreadyAlert = false;
        }

        PlayerDirector.Instance.playerData.isPlayerTurn = true;
    }

    private void LookPlayer()
    {
        var lookAtEnemy = gameObject.AddComponent<LookAtPlayer>();
        lookAtEnemy.player = PlayerDirector.Instance.Player.PlayerInstance.transform;
        lookAtEnemy.isActive = true;
    }

    private void Move()
    {
        _currentPath = MoveUtility.GetPath(
            MapManager.Instance.mapData.MapTileData, 
            new Vector2Int((int)transform.position.x / 2, (int)transform.position.z / 2),
            new Vector2Int((int)_playerPosition.x / 2, (int)_playerPosition.z / 2)
        );
        _commandInvoker.AddCommand(
        new EnemyMoveCommand(
            enemyData.Enemy,
            gameObject,
            _currentPath,
            _stateManager
        ));
        _commandInvoker.ExecuteCommand();
    }

    private void AttackPlayer()
    {
        _stateManager.SetState(EnemyState.Attack);
        var damage = CalculateDamageOutput(
            enemyData.attack,
            PlayerDirector.Instance.playerData.defense
        );
        PlayerDirector.Instance.playerData.currentHealth -= damage;
        
        _stateManager.SetState(EnemyState.Aggro); 
        Debug.Log("Damage = " + damage);
        if (InputManager.Instance.isLifeSteel)
        {
            InputManager.Instance.lifeSteelCount--;
            if (InputManager.Instance.lifeSteelCount == 0) InputManager.Instance.isLifeSteel = false;
        }
    }
    
    private static float CalculateDamageOutput(
        float attack, 
        float defense, 
        float randomFactorRange = 0.1f)
    {
        var defenseScalingFactor = Random.Range(20, 50);
        var defenseFactor = 1 - (defense / (defense + defenseScalingFactor));
        var baseDamage = attack * defenseFactor;
        var randomFactor = Random.Range(1 - randomFactorRange, 1 + randomFactorRange);
        
        return baseDamage * randomFactor;;
    }

    private void CheckStats()
    {
        infoDisplay.UpdateInfo(
            enemyData.currentHealth, 
            enemyData.maxHealth
        );

        if (enemyData.currentHealth <= 0)
        {
            EnemyDirector.Instance.EnemyList.Remove(enemyData.Enemy);
            EnemyDirector.Instance.enemyCount--;
            Destroy(gameObject);
        }
    }
}