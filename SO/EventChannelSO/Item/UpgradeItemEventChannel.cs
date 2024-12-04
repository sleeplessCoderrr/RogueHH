using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItemEventChannel", menuName = "SO/EventChannel/UpgradeItemEventChannel")]
public class UpgradeItemEventChannel : ScriptableObject
{
    public delegate void UpgradeItemEvent(UpgradeableItem item);
    public event UpgradeItemEvent OnUpgradeRequested;

    public void RaiseEvent(UpgradeableItem item)
    {
        OnUpgradeRequested?.Invoke(item);
    }
}