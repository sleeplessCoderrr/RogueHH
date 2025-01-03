using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public Vector3 playerPosition;
    public int playerLevel = 1;
    public float attack = 9f;
    public float defense = 8f;
    public int expPoint = 0;
    public int critRate = 5;
    public int critDamage = 20;     
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    
    public int zhen = 100;
    public int selectedLevel = 1;
    public List<int> playerFloorLevel = new List<int>() {1};
    public GoldUpdateEventChannel goldUpdateEventChannel;
    public int Zhen
    {
        get => zhen;
        set
        {
            if (zhen != value)
            {
                zhen = value;
                goldUpdateEventChannel?.RaiseEvent(zhen);
            }
        }
    }
    
    public PlayerData() { if (playerFloorLevel.Count == 0) playerFloorLevel.Add(1);}
}