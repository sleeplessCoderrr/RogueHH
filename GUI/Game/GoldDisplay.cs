using UnityEngine;
using UnityEngine.UI;

public class GoldDisplay : MonoBehaviour
{
    public PlayerData playerData;
    public Text goldText;

    private int previousGold = -1; 

    void Update()
    {
        if (playerData.Zhen != previousGold)
        {
            previousGold = playerData.Zhen;
            goldText.text = $"{playerData.Zhen}";
        }
    }
}