using UnityEngine;

public static class DataUpgrader
{
    public static void Upgrade(PlayerData playerData, string name, UpgradeableItem[] items)
    {
        switch (name)
        {
            case "Health Up":
                playerData.currentHealth += 10;
                playerData.maxHealth += 10;
                items[0].upgradeCost += 50;
                items[1].upgradeCost += 10;
                items[2].upgradeCost += 10;
                items[3].upgradeCost += 10;
                items[4].upgradeCost += 10;
                return;
            case "Attack Up":
                playerData.attack += 2f;
                items[0].upgradeCost += 10;
                items[1].upgradeCost += 50;
                items[2].upgradeCost += 10;
                items[3].upgradeCost += 10;
                items[4].upgradeCost += 10;
                return;
            case "Defense Up":
                playerData.defense += 5f;
                items[0].upgradeCost += 10;
                items[1].upgradeCost += 10;
                items[2].upgradeCost += 50;
                items[3].upgradeCost += 10;
                items[4].upgradeCost += 10;
                return;
            case "Luck Up":
                playerData.critRate += 2;
                items[0].upgradeCost += 10;
                items[1].upgradeCost += 10;
                items[2].upgradeCost += 10;
                items[3].upgradeCost += 50;
                items[4].upgradeCost += 10;
                return;
            case "Crit Dmg Up":
                playerData.critDamage += 5;
                items[0].upgradeCost += 10;
                items[1].upgradeCost += 10;
                items[2].upgradeCost += 10;
                items[3].upgradeCost += 10;
                items[4].upgradeCost += 50;
                return;
        }
    }
}