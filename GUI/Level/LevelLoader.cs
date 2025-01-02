using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public LevelSelectionEventChannel levelSelectionEventChannel;

    private void OnEnable()
    {
        levelSelectionEventChannel.OnLevelSelected += LoadLevel;
    }

    private void OnDisable()
    {
        levelSelectionEventChannel.OnLevelSelected -= LoadLevel;
    }

    private void LoadLevel(string levelName)
    {
        Debug.Log("Loading level: " + levelName);
        // ## TODO: Select the level dynamic
        // SceneManager.LoadScene(levelName);
    }
}