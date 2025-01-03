using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ExperienceUpdateEventChannel", menuName = "SO/EventChannel/ExperienceUpdateEventChannel")]
public class ExperienceUpdateEventChannel : ScriptableObject
{
    public UnityAction<int, int> OnExperienceUpdated;
    public void RaiseEvent(int currentExp, int maxExp)
    {
        OnExperienceUpdated?.Invoke(currentExp, maxExp);
    }
}