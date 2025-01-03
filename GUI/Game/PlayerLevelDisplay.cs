using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelDisplay : MonoBehaviour
{
    public PlayerData playerData;  
    public Text levelText;       
    public PlayerLevelUpdateEventChannel playerLevelUpdateEventChannel;  

    private void OnEnable() 
    {
        playerLevelUpdateEventChannel.OnPlayerLevelUpdated += UpdatePlayerLevelText;
        UpdatePlayerLevelText(playerData.PlayerLevel);
    }

    private void OnDisable()
    {
        playerLevelUpdateEventChannel.OnPlayerLevelUpdated -= UpdatePlayerLevelText;
    }

    private void UpdatePlayerLevelText(int level)
    {
        levelText.text = "Player Level: " + level;
    }
}