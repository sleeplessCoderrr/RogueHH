using UnityEngine;

[CreateAssetMenu(fileName = "CheatCodeEventChannel", menuName = "SO/EventChannel/CheatCodeEventChannel")]
public class CheatCodeEventChannel : ScriptableObject
{
    public delegate void CheatCodeEvent(string cheatCode);
    public event CheatCodeEvent OnCheatCodeEntered;

    public void RaiseEvent(string cheatCode)
    {
        OnCheatCodeEntered?.Invoke(cheatCode);
    }
}