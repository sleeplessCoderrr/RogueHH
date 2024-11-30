using UnityEngine;
using System.Collections.Generic;
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

    private EnemyBuilder _enemyBuilder;
    private Enemy _enemy;
    
    public Animator animator;
    public GameObject[] enemyInstance;

    private async void Awake()
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
        
        await Task.Delay(3000);
        InitializeEnemy();
    }

    private async void InitializeEnemy()
    {
        await Task.Delay(1000);
        _enemyBuilder = new EnemyBuilder();
        _enemy = new Enemy(enemyConfig, enemyData, animator);
        
        _enemyBuilder.SetParent(transform);
        enemyInstance = _enemyBuilder
            .SetData(enemyConfig, enemyData)
            .Build(mapConfig, mapData, 2);
    }

    private void SetAnimator()
    {
        
    }
}