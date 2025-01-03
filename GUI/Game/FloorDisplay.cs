using UnityEngine;
using UnityEngine.UI;

public class FloorDisplay : MonoBehaviour
{
    public PlayerData playerData;  
    public Text floorText;        
    public LevelUpdateEventChannel levelUpdateEventChannel;  

    private void OnEnable()
    {
        levelUpdateEventChannel.OnLevelUpdated += UpdateFloorText;
    }

    private void OnDisable()
    {
        levelUpdateEventChannel.OnLevelUpdated -= UpdateFloorText;
    }

    private void Update()
    {
        if (playerData != null)
        {
            UpdateFloorText(playerData.SelectedLevel);
        }
    }

    private void UpdateFloorText(int newLevel)
    {
        if (newLevel == -1)
        {
            floorText.text = "Boss Floor";  
        }
        else
        {
            floorText.text = "Floor " + newLevel; 
        }
    }
}