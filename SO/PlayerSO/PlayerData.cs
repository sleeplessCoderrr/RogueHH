using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public Vector2 playerPosition;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
}