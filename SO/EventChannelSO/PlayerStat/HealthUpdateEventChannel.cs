using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HealthUpdateEventChannel", menuName = "SO/EventChannel/HealthUpdateEventChannel")]
public class HealthUpdateEventChannel : ScriptableObject
{
    public UnityAction<float, float> OnHealthUpdated;
    public void RaiseEvent(float currentHealth, float maxHealth)
    {
        OnHealthUpdated?.Invoke(currentHealth, maxHealth);
    }
}