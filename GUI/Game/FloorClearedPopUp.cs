using UnityEngine;

public class FloorClearedPopUp : MonoBehaviour
{
    public GameObject floorClearedCanvas; 
    private int _enemyCount;

    void Start()
    {
        floorClearedCanvas.SetActive(false); 
    }

    void Update()
    {
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        _enemyCount = EnemyDirector.Instance.enemyCount;
        if (_enemyCount == 0)
        {
            ShowFloorClearedPopup();
        }
    }

    private void ShowFloorClearedPopup()
    {
        floorClearedCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}