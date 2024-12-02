using UnityEngine;

public class Enemy
{
    [Header("Enemy SO")]
    public EnemyConfig EnemyConfig;
    public EnemyData EnemyData;
    
    [Header("Enemy Data")]
    public Animator Animator;  
    public GameObject EnemiesInstance;

    public Enemy(EnemyConfig enemyConfig, EnemyData enemyData)
    {
        this.EnemyConfig = enemyConfig;
        this.EnemyData = enemyData;
    }
}