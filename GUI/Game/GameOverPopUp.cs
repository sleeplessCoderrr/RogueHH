using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    public GameObject gameOverPopUp;

    private void Update()
    {
        if (PlayerDirector.Instance.playerData.currentHealth <= 0)
        {
            gameOverPopUp.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
