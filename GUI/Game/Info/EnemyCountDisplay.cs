using UnityEngine;
using UnityEngine.UI;

public class EnemyCountDisplay : MonoBehaviour
{
    public Text enemyCountText;
    public EnemyCountUpdateEventChannel enemyCountUpdateEventChannel;

    private void OnEnable()
    {
        enemyCountUpdateEventChannel.OnEnemyCountUpdated += UpdateEnemyCountText;
    }

    private void OnDisable()
    {
        enemyCountUpdateEventChannel.OnEnemyCountUpdated -= UpdateEnemyCountText;
    }

    private void UpdateEnemyCountText(int count)
    {
        enemyCountText.text = $"Enemies left: {count}";
    }
}