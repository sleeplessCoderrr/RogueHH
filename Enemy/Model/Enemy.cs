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

    public IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(EnemiesInstance.transform.position, targetPosition) > 0.01f)
        {
            this.WalkToward(targetPosition);
            yield return null;
        }
    }

    private void WalkToward(Vector3 targetPosition)
    {
        var rigidBody = EnemiesInstance.GetComponent<Rigidbody>();
        if (rigidBody == null) Debug.Log("Rigidbody null");
        
        var direction  = (targetPosition - EnemiesInstance.transform.position).normalized;
        rigidBody.velocity = direction * EnemyConfig.walkSpeed;

        if (Vector3.Distance(EnemiesInstance.transform.position, targetPosition) > 0.01f)
        {
            rigidBody.velocity = Vector3.zero;
            EnemiesInstance.transform.position = targetPosition;
        }
    }
}