using UnityEngine;

public class Enemy
{
    [Header("Enemy Config")]
    public EnemyConfig EnemyConfig;
    
    [Header("Enemy Data")]
    public EnemyData EnemyData;

    public Enemy(EnemyConfig enemyConfig, EnemyData enemyData)
    {
        this.EnemyConfig = enemyConfig;
        this.EnemyData = enemyData;
    }
}