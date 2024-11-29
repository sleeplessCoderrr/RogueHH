using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "SO/Config/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("Enemy Components")] 
    public GameObject[] enemyPrefabs;
    public Animator animator;
    
    [Header("Enemy Settings")]
    public float walkSpeed = 5f;
}