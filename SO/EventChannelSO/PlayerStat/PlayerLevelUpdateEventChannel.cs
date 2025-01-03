using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerLevelUpdateEventChannel", menuName = "SO/EventChannel/PlayerLevelUpdateEventChannel")]
public class PlayerLevelUpdateEventChannel : ScriptableObject
{
    public UnityAction<int> OnPlayerLevelUpdated;

    public void RaiseEvent(int level)
    {
        OnPlayerLevelUpdated?.Invoke(level);
    }
}