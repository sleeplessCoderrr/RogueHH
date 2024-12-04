using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionManager : MonoBehaviour
{
    [SerializeField] private UpgradeableItemUI[] itemUIList;
    [SerializeField] private ItemDetailDisplay itemDetailsDisplay;

    private UpgradeableItemUI _currentSelectedItemUI;

    private void Start()
    {
        foreach (var itemUI in itemUIList)
        {
            itemUI.GetComponent<Button>().onClick.AddListener(() => HandleSelection(itemUI));
        }
    }

    private void HandleSelection(UpgradeableItemUI selectedItemUI)
    {
        if (_currentSelectedItemUI != null)
        {
            _currentSelectedItemUI.Deselect();
        }

        _currentSelectedItemUI = selectedItemUI;
        _currentSelectedItemUI.Select();

        itemDetailsDisplay.UpdateItemDetails(_currentSelectedItemUI.GetItemData());
    }
}