using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EnemyCountUpdateEventChannel", menuName = "SO/EventChannel/EnemyCountUpdateEventChannel")]
public class EnemyCountUpdateEventChannel : ScriptableObject
{
    public UnityAction<int> OnEnemyCountUpdated;
    public void RaiseEvent(int count)
    {
        OnEnemyCountUpdated?.Invoke(count);
    }
}
