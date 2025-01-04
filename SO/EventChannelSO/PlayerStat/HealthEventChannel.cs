using UnityEngine;

[CreateAssetMenu(menuName = "SO/EventChannel/HealthEventChannel")]
public class HealthEventChannel : ScriptableObject
{
    public delegate void HealthChanged(float currentHealth, float maxHealth);
    public event HealthChanged OnHealthChanged;
    public void RaiseEvent(float currentHealth, float maxHealth)
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}