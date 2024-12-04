using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private ItemDetailDisplay itemDetailsDisplay; // Panel showing the item details
    [SerializeField] private UpgradeItemManager upgradeManager;    // Manager handling upgrade logic
    [SerializeField] private PlayerData playerData;                // Player data to show the current gold

    public void OnUpgradeClick()
    {
        var selectedItem = itemDetailsDisplay.GetCurrentItem();
        if (selectedItem != null)
        {
            if (playerData.zhen >= selectedItem.upgradeCost) 
            {
                upgradeManager.HandleUpgrade(selectedItem);
                itemDetailsDisplay.UpdateItemDetails(selectedItem);
            }
            else
            {
                Debug.Log("Not enough gold to upgrade!");
            }
        }
        else
        {
            Debug.Log("No item selected to upgrade!");
        }
    }
}