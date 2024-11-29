using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Vector3Int enemyPosition;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
}
