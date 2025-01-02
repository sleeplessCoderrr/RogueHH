using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/EventChannel/LevelSelectionEventChannel")]
public class LevelSelectionEventChannel : ScriptableObject
{
    public UnityAction<string> OnLevelSelected;

    public void RaiseEvent(string levelName)
    {
        if (OnLevelSelected != null)
        {
            OnLevelSelected.Invoke(levelName);
        }
        else
        {
            Debug.LogWarning("No listeners for the LevelSelectionEventChannel!");
        }
    }
}