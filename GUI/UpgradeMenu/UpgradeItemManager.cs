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
        if (!item.UpgradeAble()) return;
        if (playerData.zhen < item.upgradeCost) return;

        playerData.zhen -= item.upgradeCost;
        item.Upgrade();
        itemDetailDisplay.UpdateItemDetails(item);
    }
}