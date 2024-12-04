using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject popUp; 

    private void Start()
    {
        if (popUp != null)
            popUp.SetActive(false);
    }

    public void ShowPopUp()
    {
        if (popUp != null)
            popUp.SetActive(true);
    }

    public void HidePopUp()
    {
        if (popUp != null)
        {
            popUp.SetActive(false);
        }
    }

    public void GoToUpgradeMenu()
    {
        SceneManager.LoadScene("UpgradeMenu");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
}