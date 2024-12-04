using UnityEngine;

public class UpgradeItemManager : MonoBehaviour
{
    [SerializeField] private UpgradeItemEventChannel upgradeItemEventChannel;
    [SerializeField] private PlayerData playerData;

    private void OnEnable()
    {
        if (upgradeItemEventChannel != null)
            upgradeItemEventChannel.OnUpgradeRequested += HandleUpgrade;
    }

    private void OnDisable()
    {
        if (upgradeItemEventChannel != null)
            upgradeItemEventChannel.OnUpgradeRequested -= HandleUpgrade;
    }

    private void HandleUpgrade(UpgradeableItem item)
    {
        if (!item.UpgradeAble())
        {
            Debug.Log("Item is at max level!");
            return;
        }

        if (playerData.gold < item.upgradeCost)
        {
            Debug.Log("Not enough gold to upgrade!");
            return;
        }

        playerData.gold -= item.upgradeCost;
        item.Upgrade();
        ApplyUpgradeBenefits(item);

        Debug.Log($"{item.itemName} upgraded to level {item.currentLevel}!");
    }

    private void ApplyUpgradeBenefits(UpgradeableItem item)
    {
        // Example: Apply benefits based on the item type or level.
        // This could modify player stats, abilities, etc.
        Debug.Log($"Applying benefits: {item.benefitDescription}");
    }
}