using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/EventChannel/Gold Update EventChannel")]
public class GoldUpdateEventChannel : ScriptableObject
{
    public UnityEngine.Events.UnityAction<int> OnGoldUpdated;

    public void RaiseEvent(int newGoldAmount)
    {
        OnGoldUpdated?.Invoke(newGoldAmount);
    }
}