using System.Collections;
using UnityEngine;

public class Enemy
{
    [Header("Enemy SO")]
    public EnemyConfig EnemyConfig;
    
    [Header("Enemy Data")]
    public Animator Animator;  
    public GameObject EnemiesInstance;

    public Enemy(EnemyConfig enemyConfig)
    {
        this.EnemyConfig = enemyConfig;
    }
}