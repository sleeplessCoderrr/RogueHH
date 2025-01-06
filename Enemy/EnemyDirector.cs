using UnityEngine;
using System.Threading.Tasks;

public class EnemyDirector : MonoBehaviour
{
    public static EnemyDirector Instance;

    [Header("Enemy Config & Data")]
    public EnemyConfig enemyConfig;
    
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
            var enemyData = ScriptableObject.CreateInstance<EnemyData>();
            var enemy = new Enemy(enemyConfig);
            enemy.EnemiesInstance = _enemyInstanceList[i];
            enemy.EnemiesInstance.GetComponent<EnemyStateManager>().SetEnemyEntity(enemy);
            _enemyList[i] = enemy;
        }
    }

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
