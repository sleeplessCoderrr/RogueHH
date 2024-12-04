using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeableItemUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemLevelText;
    [SerializeField] private UpgradeableItem itemData;
    [SerializeField] private UpgradeItemEventChannel upgradeItemEventChannel;

    private bool _isSelected;

    private void Start()
    {
        Deselect();
    }

    private void Update()
    {
        UpdateItemUI();
    }

    public void OnItemClicked()
    {
        if (!_isSelected)
        {
            upgradeItemEventChannel.RaiseEvent(itemData);
        }
    }

    public void Select()
    {
        _isSelected = true;
    }

    public void Deselect()
    {
        _isSelected = false;
    }

    private void UpdateItemUI()
    {
        if (itemData == null) return;
        itemLevelText.text = $"Lvl. {itemData.currentLevel}/{itemData.maxLevel}";
        itemImage.sprite = itemData.itemSprite; 
    }


    public UpgradeableItem GetItemData()
    {
        return itemData;
    }
}