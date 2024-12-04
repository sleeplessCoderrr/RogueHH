using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public Vector3 playerPosition;
    public float attack = 9f;
    public float defense = 8f;
    public int playerLevel = 1;
    public int expPoint = 0;
    public int critRate = 5;
    public int critDamage = 20;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    [FormerlySerializedAs("gold")] public int zhen = 100;
}