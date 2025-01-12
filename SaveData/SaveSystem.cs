using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public PlayerData playerData;
    public UpgradeableItem[] items;
    public PlayerSaveData playerSaveData;
    private string _savePath;

    private void Start()
    {
        _savePath = Application.persistentDataPath + "/saveData.json";
    }
    
    public void SaveGame()
    {
        var saveData = new SaveData
        {
            playerSaveData = new PlayerSaveData
            {
                Attack = playerData.attack,
                Defense = playerData.defense,
                PlayerLevel = playerData.playerLevel,
                MaxExpPoint = playerData.maxExpPoint,
                CurrentExpPoint = playerData.currentExpPoint,
                CriticalRate = playerData.critRate,
                CriticalDamage = playerData.critDamage,
                MaxHealth = playerData.maxHealth,
                CurrentHealth = playerData.currentHealth,
                Zhen = playerData.zhen,
                PlayerFloorLevel = new List<int>(playerData.playerFloorLevel)
            }
        };

        foreach (var item in items)
        {
            saveData.upgradeAbleItemSaveData.Add(new UpgradeAbleItemSaveData
            {
                itemName = item.itemName,
                currentLevel = item.currentLevel,
                upgradeCost = item.upgradeCost,
            });
        }

        var json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(_savePath, json);

        Debug.Log($"Game saved to {_savePath}");
    }
    
    public void LoadGame()
    {
        if (File.Exists(_savePath))
        {
            var json = File.ReadAllText(_savePath);
            var saveData = JsonUtility.FromJson<SaveData>(json);

            playerData.attack = saveData.playerSaveData.Attack;
            playerData.defense = saveData.playerSaveData.Defense;
            playerData.playerLevel = saveData.playerSaveData.PlayerLevel;
            playerData.maxExpPoint = saveData.playerSaveData.MaxExpPoint;
            playerData.currentExpPoint = saveData.playerSaveData.CurrentExpPoint;
            playerData.critRate = saveData.playerSaveData.CriticalRate;
            playerData.critDamage = saveData.playerSaveData.CriticalDamage;
            playerData.maxHealth = saveData.playerSaveData.MaxHealth;
            playerData.currentHealth = saveData.playerSaveData.CurrentHealth;
            playerData.zhen = saveData.playerSaveData.Zhen;
            playerData.playerFloorLevel = new List<int>(saveData.playerSaveData.PlayerFloorLevel);

            foreach (var itemData in saveData.upgradeAbleItemSaveData)
            {
                foreach (var item in items)
                {
                    if (item.itemName == itemData.itemName)
                    {
                        item.currentLevel = itemData.currentLevel;
                        item.upgradeCost = itemData.upgradeCost;
                    }
                }
            }

            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.LogWarning("No save file found!");
        }
    }

}