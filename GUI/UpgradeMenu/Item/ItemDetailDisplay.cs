using UnityEngine;
using UnityEngine.UI;

public class ItemDetailDisplay : MonoBehaviour
{
    [SerializeField] private Text itemNameText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemDescriptionText;
    [SerializeField] private Text upgradeCostText;
    [SerializeField] private Text playerGoldText;
    [SerializeField] private Text benefitsText;
    
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UpgradeableItem _currentItem;

    private void Update()
    {
        UpdateItemDetails(_currentItem);
    }
    
    public void UpdateItemDetails(UpgradeableItem item)
    {
        _currentItem = item;
        itemNameText.text = item.itemName;
        itemImage.sprite = item.itemSprite;
        itemDescriptionText.text = item.benefitDescription;
        upgradeCostText.text = $"{item.upgradeCost} To Upgrade";
        playerGoldText.text = $"{playerData.zhen}";
        if(item.itemName == "Health Up") benefitsText.text = $"Current: {playerData.currentHealth} hp                                    Upgrade: +10 hp";
        if(item.itemName == "Attack Up") benefitsText.text = $"Current: {playerData.attack} atk                                     Upgrade: +2 atk";
        if(item.itemName == "Defense Up") benefitsText.text = $"Current: {playerData.defense} def                                     Upgrade: +2 def";
        if(item.itemName == "Luck Up") benefitsText.text = $"Current: {playerData.critRate}% rate                                     Upgrade: +2% rate";
        if(item.itemName == "Crit Dmg Up") benefitsText.text = $"Current: {playerData.critDamage}% dmg                                     Upgrade: +2% dmg";
    }

    public UpgradeableItem GetCurrentItem()
    {
        return _currentItem;
    }
}