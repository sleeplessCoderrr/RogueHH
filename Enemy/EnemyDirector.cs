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
        const int enemyCount = 10;
        await Task.Delay(1000);

        _enemyBuilder = new EnemyBuilder();
        _enemyList = new Enemy[enemyCount];
        
        _enemyBuilder.SetParent(transform);
        _enemyBuilder.SetData(enemyConfig);
        _enemyInstanceList = _enemyBuilder.Build(mapConfig, mapData, 10);
        
        await Task.Delay(3000);
        AddInstance(enemyCount);
    }

    private void AddInstance(int count)
    {
        for (var i = 0; i < count; i++)
        {
            if(!_enemyInstanceList[1])Debug.Log("Kosong");
            var enemy = new Enemy(enemyConfig, enemyData);
            enemy.EnemiesInstance = _enemyInstanceList[i];
            // Debug.Log(_enemyInstanceList[i]);
            enemy.EnemiesInstance.GetComponent<EnemyStateManager>().SetEnemyEntity(enemy);
            // Debug.Log(enemy.EnemiesInstance.GetComponent<EnemyStateManager>()._enemy);
            _enemyList[i] = enemy;
        }
    }
}