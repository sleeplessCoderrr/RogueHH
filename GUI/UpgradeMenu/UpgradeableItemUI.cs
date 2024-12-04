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
        UpdateItemUI();
        Deselect();
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
        if (itemData == null)
        {
            Debug.LogError("itemData is not assigned in UpgradeableItemUI.");
            return;
        }

        if (itemLevelText != null)
        {
            itemLevelText.text = $"Lvl. {itemData.currentLevel}/{itemData.maxLevel}";
        }
        else
        {
            Debug.LogError("itemLevelText is not assigned in the Inspector.");
        }

        if (itemImage != null)
        {
            itemImage.sprite = itemData.itemSprite;
        }
        else
        {
            Debug.LogError("itemImage is not assigned in the Inspector.");
        }
    }


    public UpgradeableItem GetItemData()
    {
        return itemData;
    }
}