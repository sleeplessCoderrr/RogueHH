using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public PlayerData playerData;         
    public Text healthText;             
    public HealthUpdateEventChannel healthUpdateEventChannel;  

    private void OnEnable() 
    {
        healthUpdateEventChannel.OnHealthUpdated += UpdateHealthText;
        UpdateHealthText(playerData.CurrentHealth, playerData.MaxHealth);
    }

    private void OnDisable()
    {
        healthUpdateEventChannel.OnHealthUpdated -= UpdateHealthText;
    }

    private void UpdateHealthText(float currentHealth, float maxHealth)
    {
        healthText.text = $"{currentHealth} / {maxHealth}";
    }
}