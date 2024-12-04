using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public Vector3 playerPosition;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    [FormerlySerializedAs("gold")] public int zhen = 100;
}