using UnityEngine;
using UnityEngine.UI;

public class ItemDetailDisplay : MonoBehaviour
{
    [SerializeField] private Text itemNameText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemDescriptionText;
    [SerializeField] private Text upgradeCostText;
    [SerializeField] private Text playerGoldText;
    
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UpgradeableItem _currentItem;

    public void UpdateItemDetails(UpgradeableItem item)
    {
        _currentItem = item;
        itemNameText.text = item.itemName;
        itemImage.sprite = item.itemSprite;
        itemDescriptionText.text = item.benefitDescription;
        upgradeCostText.text = $"Upgrade Cost: {item.upgradeCost}";
        playerGoldText.text = $"Gold: {playerData.zhen}";
    }

    public UpgradeableItem GetCurrentItem()
    {
        return _currentItem;
    }
}