using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuToggle : MonoBehaviour
{
    private bool _isPaused = false;
    public GameObject pauseMenuCanvas; 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        _isPaused = true;
        pauseMenuCanvas.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
        InputManager.Instance.isPaused = true;
    }

    public void ResumeGame()
    {
        _isPaused = false;
        pauseMenuCanvas.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
        InputManager.Instance.isPaused = false;
    }

    public void GotoUpgradeMenu()
    {
        SceneManager.LoadScene("UpgradeMenu");
        ResumeGame();
    }

    public void SaveAndQuit()
    {
        SceneManager.LoadScene("MainMenu");
        ResumeGame();
    }
}