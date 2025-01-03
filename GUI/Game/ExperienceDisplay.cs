using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    public PlayerData playerData;  
    public Text experienceText;   
    public ExperienceUpdateEventChannel experienceUpdateEventChannel;  

    private void OnEnable()
    {
        experienceUpdateEventChannel.OnExperienceUpdated += UpdateExperienceText;
        UpdateExperienceText(playerData.CurrentExpPoint, playerData.MaxExpPoint);
    }

    private void OnDisable()
    {
        experienceUpdateEventChannel.OnExperienceUpdated -= UpdateExperienceText;
    }

    private void UpdateExperienceText(int currentExp, int maxExp)
    {
        experienceText.text = $"{currentExp} / {maxExp}";
    }
}