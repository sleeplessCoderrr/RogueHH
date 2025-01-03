using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeableItem", menuName = "SO/Items/UpgradeableItem")]
public class UpgradeableItem : ScriptableObject
{   
    public string itemName;
    public Sprite itemSprite;
    public int currentLevel = 0;
    public int maxLevel = 45;
    public int upgradeCost = 10;
    public string benefitDescription;
    public PlayerData playerData;
    
    public void Upgrade()
    {
        if (UpgradeAble())
        {
            currentLevel += 1;
            DataUpgrader.Upgrade(playerData, itemName);
        }
    }
    public bool UpgradeAble() => currentLevel < maxLevel;
}