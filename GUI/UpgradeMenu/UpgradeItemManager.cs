using UnityEngine;

public class UpgradeItemManager : MonoBehaviour
{
    [SerializeField] private UpgradeItemEventChannel upgradeItemEventChannel;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ItemDetailDisplay itemDetailDisplay;

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

    public void HandleUpgrade(UpgradeableItem item)
    {
        if (!item.UpgradeAble())
        {
            Debug.Log("Item is already at max level!");
            return;
        }

        if (playerData.zhen < item.upgradeCost)
        {
            Debug.Log("Not enough gold to upgrade!");
            return;
        }

        playerData.zhen -= item.upgradeCost;
        item.Upgrade();
        itemDetailDisplay.UpdateItemDetails(item);
    }

    private void ApplyUpgradeBenefits(UpgradeableItem item)
    {
        Debug.Log($"Applying benefits: {item.benefitDescription}");
    }
}