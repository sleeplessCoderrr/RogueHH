using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;         

    [Header("Health Display")]
    public Text healthText;             
    public Slider healthSlider;          
    public HealthUpdateEventChannel healthUpdateEventChannel;  

    private void OnEnable() 
    {
        healthUpdateEventChannel.OnHealthUpdated += UpdateHealthDisplay;
        UpdateHealthDisplay(playerData.CurrentHealth, playerData.MaxHealth);
    }

    private void OnDisable()
    {
        healthUpdateEventChannel.OnHealthUpdated -= UpdateHealthDisplay;
    }

    private void Update()
    {
        UpdateHealthDisplay(playerData.CurrentHealth, playerData.MaxHealth);
    }

    private void UpdateHealthDisplay(float currentHealth, float maxHealth)
    {
        healthText.text = $"{currentHealth:0} / {maxHealth:0}";
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}