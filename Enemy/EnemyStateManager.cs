using UnityEditor.VersionControl;
using System.Threading.Tasks;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public enum EnemyState
{
    Idle,
    Walk
}

public class EnemyStateManager : StateManager<EnemyState>
{
    public static EnemyStateManager Instance;

    [Header("Enemy Config & Data")]
    public EnemyConfig enemyConfig;
    public EnemyData enemyData;
    
    [Header("MapConfig & Data")]
    public MapConfig mapConfig;
    public MapData mapData;

    private EnemyBuilder _enemyBuilder;
    private Enemy _enemy;
    
    private Animator _animator;
    public GameObject[] enemyInstance;

    private void Awake()
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
        
        InitializeStates();
        InitializeEnemy();
    }
    
    protected override void InitializeStates()
    {
        // States[EnemyState.Idle] = new
    }

    private async void InitializeEnemy()
    {
        await Task.Delay(1000);
        _enemyBuilder = new EnemyBuilder();
        _enemy = new Enemy(enemyConfig, enemyData);
        
        _enemyBuilder.SetParent(transform);
        enemyInstance = _enemyBuilder
                        .SetData(enemyConfig, enemyData)
                        .Build(mapConfig, mapData, 2);
    }

    private void SetAnimator()
    {
        
    }
}