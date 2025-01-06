using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EventChannel/EnemyStateChangeEventChannel")]
public class EnemyStateChangeEventChannel : ScriptableObject
{
    public Action<EnemyState> OnStateChanged;

    public void RaiseEvent(EnemyState state)
    {
        OnStateChanged?.Invoke(state);
    }
}