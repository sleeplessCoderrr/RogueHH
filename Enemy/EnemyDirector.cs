using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EnemyDirector : MonoBehaviour
{
    public static EnemyDirector Instance;

    [Header("Enemy Config & Data")]
    public EnemyConfig enemyConfig;
    public EnemyData enemyData;
    
    [Header("MapConfig & Data")]
    public MapConfig mapConfig;
    public MapData mapData;

    public int enemyCount;
    public EnemyCountUpdateEventChannel enemyCountUpdateEventChannel;

    private GameObject[] _enemyInstanceList;
    private EnemyBuilder _enemyBuilder;
    private Enemy[] _enemyList;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        InitializeEnemy();
    }

    private async void InitializeEnemy()
    {
        enemyCount = 10;
        NotifyEnemyCount(); 
        await Task.Delay(1000);

        _enemyBuilder = new EnemyBuilder();
        _enemyList = new Enemy[enemyCount];
        
        _enemyBuilder.SetParent(transform);
        _enemyBuilder.SetData(enemyConfig);
        _enemyInstanceList = _enemyBuilder.Build(mapConfig, mapData, enemyCount);
        
        await Task.Delay(3000);
        AddInstance(enemyCount);
    }

    private void AddInstance(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var enemy = new Enemy(enemyConfig, enemyData);
            enemy.EnemiesInstance = _enemyInstanceList[i];
            enemy.EnemiesInstance.GetComponent<EnemyStateManager>().SetEnemyEntity(enemy);
            _enemyList[i] = enemy;
        }
    }

    // private void Update()
    // {
    //     foreach (var enemy in _enemyList)
    //     {
    //         
    //     }
    // }

    public void OnEnemyDefeated()
    {
        enemyCount--;
        NotifyEnemyCount();
    }

    private void NotifyEnemyCount()
    {
        enemyCountUpdateEventChannel?.RaiseEvent(enemyCount);
    }
}
