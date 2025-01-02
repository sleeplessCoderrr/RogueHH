 using UnityEngine;
 using UnityEngine.SceneManagement;

 public class UpgradeMenuManager : MonoBehaviour
 {
     public void GoToMainMenu()
     {
         SceneManager.LoadScene("MainMenu");
     }

     public void GoToGame()
     {
         SceneManager.LoadScene("Game");
     }
 }