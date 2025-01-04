using UnityEngine;

[CreateAssetMenu(menuName = "SO/EventChannel/ExpEventChannel")]
public class ExpEventChannel : ScriptableObject
{
    public delegate void ExpChanged(float currentExp, float expToNextLevel);
    public event ExpChanged OnExpChanged;

    public void RaiseEvent(float currentExp, float expToNextLevel)
    {
        OnExpChanged?.Invoke(currentExp, expToNextLevel);
    }
}