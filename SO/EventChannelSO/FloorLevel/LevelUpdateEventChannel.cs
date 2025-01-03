using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LevelUpdateEventChannel", menuName = "SO/EventChannel/LevelUpdateEventChannel")]
public class LevelUpdateEventChannel : ScriptableObject
{
    public UnityAction<int> OnLevelUpdated;

    public void RaiseEvent(int level)
    {
        if (OnLevelUpdated != null)
            OnLevelUpdated.Invoke(level);
    }
}