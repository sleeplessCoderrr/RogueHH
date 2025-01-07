using System;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;  

    [Header("Experience Display")]
    public Text experienceText;   
    public Slider experienceSlider;  
    public ExperienceUpdateEventChannel experienceUpdateEventChannel;  

    private void OnEnable()
    {
        experienceUpdateEventChannel.OnExperienceUpdated += UpdateExperienceDisplay;
        UpdateExperienceDisplay(playerData.CurrentExpPoint, playerData.MaxExpPoint);
    }

    private void OnDisable()
    {
        experienceUpdateEventChannel.OnExperienceUpdated -= UpdateExperienceDisplay;
    }

    private void Update()
    {
        UpdateExperienceDisplay(playerData.CurrentExpPoint, playerData.MaxExpPoint);
    }

    private void UpdateExperienceDisplay(int currentExp, int maxExp)
    {
        experienceText.text = $"{currentExp} / {maxExp}";
        if (experienceSlider != null)
        {
            experienceSlider.value = (float)currentExp / maxExp;
        }
    }
}