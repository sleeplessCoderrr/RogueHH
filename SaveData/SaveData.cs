using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public PlayerSaveData playerSaveData;
    public List<UpgradeAbleItemSaveData> upgradeAbleItemSaveData;
}

public class PlayerSaveData
{
    public float Attack;
    public float Defense;
    public int PlayerLevel;
    public int MaxExpPoint;
    public int CurrentExpPoint;
    public int CriticalRate;
    public int CriticalDamage;
    public float MaxHealth;
    public float CurrentHealth;
    public int Zhen;
    public List<int> PlayerFloorLevel;
}

public class UpgradeAbleItemSaveData
{
    public string itemName;
    public int currentLevel = 0;
    public int maxLevel = 45;
    public int upgradeCost = 10;  
}