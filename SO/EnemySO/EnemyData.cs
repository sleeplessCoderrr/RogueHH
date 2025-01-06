using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Vector3Int enemyPosition;
    public float maxHealth = 20f;
    public float currentHealth = 20f;
}
