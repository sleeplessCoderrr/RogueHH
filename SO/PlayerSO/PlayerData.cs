using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public Vector3 playerPosition;
    public int playerLevel = 1;
    public float attack = 5f;
    public float defense = 5f;
    public int currentExpPoint = 0;
    public int maxExpPoint = 5;
    public int critRate = 5;
    public int critDamage = 150;     
    public float maxHealth = 20f;
    public float currentHealth = 20f;

    public bool isPlayerTurn = true;
    
    public int zhen = 100;
    public int selectedLevel = 1;
    public List<int> playerFloorLevel = new List<int>() {1};
    public GoldUpdateEventChannel goldUpdateEventChannel;
    public LevelUpdateEventChannel levelUpdateEventChannel;
    public PlayerLevelUpdateEventChannel playerLevelUpdateEventChannel;
    public ExperienceUpdateEventChannel experienceUpdateEventChannel;
    public HealthUpdateEventChannel healthUpdateEventChannel;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (Mathf.Abs(currentHealth - value) > Mathf.Epsilon) 
            {
                currentHealth = value;
                healthUpdateEventChannel?.RaiseEvent(currentHealth, maxHealth);
            }
        }
    }

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            if (Mathf.Abs(maxHealth - value) > Mathf.Epsilon)
            {
                maxHealth = value;
                healthUpdateEventChannel?.RaiseEvent(currentHealth, maxHealth);
            }
        }
    }
    
    public int CurrentExpPoint
    {
        get => currentExpPoint;
        set
        {
            if (currentExpPoint != value)
            {
                currentExpPoint = value;
                experienceUpdateEventChannel?.RaiseEvent(currentExpPoint, maxExpPoint);
                CheckLevelUp();
            }
        }
    }

    public void CheckLevelUp()
    {
        while (currentExpPoint >= maxExpPoint) 
        {
            currentExpPoint -= maxExpPoint; 
            PlayerLevel++;
            attack += 3;
            defense += 2;
            currentHealth = maxHealth = maxHealth + 10;
            
            maxExpPoint = CalculateNextLevelExp(maxExpPoint);
            experienceUpdateEventChannel?.RaiseEvent(currentExpPoint, maxExpPoint); 
        }
    }

    private int CalculateNextLevelExp(int currentMaxExp)
    {
        return Mathf.RoundToInt(currentMaxExp * 1.2f);
    }


    public int MaxExpPoint
    {
        get => maxExpPoint;
        set
        {
            if (maxExpPoint != value)
            {
                maxExpPoint = value;
                experienceUpdateEventChannel?.RaiseEvent(currentExpPoint, maxExpPoint);
            }
        }
    }
    
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
    
    public int SelectedLevel
    {
        get => selectedLevel;
        set
        {
            if (selectedLevel != value)
            {
                selectedLevel = value;
                levelUpdateEventChannel?.RaiseEvent(selectedLevel); 
            }
        }
    }
    
    public int PlayerLevel
    {
        get => playerLevel;
        set
        {
            if (playerLevel != value)
            {
                playerLevel = value;
                playerLevelUpdateEventChannel?.RaiseEvent(playerLevel);
            }
        }
    }
    
    public PlayerData() { if (playerFloorLevel.Count == 0) playerFloorLevel.Add(1);}
}