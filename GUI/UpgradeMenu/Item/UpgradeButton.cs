using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private ItemDetailDisplay itemDetailsDisplay;
    [SerializeField] private UpgradeItemManager upgradeManager;   
    [SerializeField] private PlayerData playerData;                

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