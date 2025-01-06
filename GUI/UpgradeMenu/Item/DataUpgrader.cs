using UnityEngine;

public static class DataUpgrader
{
    
    public static void Upgrade(PlayerData playerData, string name)
    {
        switch (name)
        {
            case "Health Up":
                playerData.currentHealth += 10;
                playerData.maxHealth += 10;
                return;
            case "Attack Up":
                playerData.attack += 2f;
                return;
            case "Defense Up":
                playerData.defense += 2f;
                return;
            case "Luck Up":
                playerData.critRate += 2;
                return;
            case "Crit Dmg Up":
                playerData.critDamage += 5;
                return;
        }
    }
}