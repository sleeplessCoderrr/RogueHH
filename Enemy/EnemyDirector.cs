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
    public Enemy[] EnemyList;

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
        enemyCount = MapUtility.CalculateEnemies(
            PlayerDirector.Instance.playerData.selectedLevel
        );
        NotifyEnemyCount(); 
        await Task.Delay(1000);

        _enemyBuilder = new EnemyBuilder();
        EnemyList = new Enemy[enemyCount];
        
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
            var enemy = new Enemy(enemyConfig);
            enemy.EnemiesInstance = _enemyInstanceList[i];
            enemy.EnemiesInstance.GetComponent<EnemyStateManager>().SetEnemyEntity(enemy);
            EnemyList[i] = enemy;
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
