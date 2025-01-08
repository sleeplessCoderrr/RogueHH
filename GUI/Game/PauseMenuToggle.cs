using UnityEngine;

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
        _isPaused = !_isPaused;
        pauseMenuCanvas.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
        InputManager.Instance.isPaused = !InputManager.Instance.isPaused;
    }
}