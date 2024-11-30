using UnityEngine;

public class Enemy
{
    [Header("Enemy Config")]
    public EnemyConfig EnemyConfig;
    
    [Header("Enemy Data")]
    public EnemyData EnemyData;
    
    public Animator Animator;

    public Enemy(EnemyConfig enemyConfig, EnemyData enemyData, Animator animator)
    {
        this.EnemyConfig = enemyConfig;
        this.EnemyData = enemyData;
        this.Animator = animator;
    }
}