using UnityEngine;

public class CheatCodeListener : MonoBehaviour
{
    [SerializeField] private CheatCodeEventChannel cheatCodeEventChannel;
    [SerializeField] private PlayerData playerData; 

    private void OnEnable()
    {
        if (cheatCodeEventChannel != null)
            cheatCodeEventChannel.OnCheatCodeEntered += HandleCheatCode;
    }

    private void OnDisable()
    {
        if (cheatCodeEventChannel != null)
            cheatCodeEventChannel.OnCheatCodeEntered -= HandleCheatCode;
    }

    private void HandleCheatCode(string cheatCode)
    {
        switch (cheatCode)
        {
            case "hesoyam":
                playerData.expPoint += 1000;
                break;
            case "tpagamegampang":
                playerData.zhen += 1000;
                break;
            case "opensesame":
                for (int i = 1; i <= 100; i++)
                {
                    if (!playerData.playerLevel.Contains(i))
                    {
                        playerData.playerLevel.Add(i);
                    }
                }
                Debug.Log("All levels from 1 to 100 have been unlocked!");
                DropdownHandler.Instance.PopulateDropdown();
                break;
        }
    }
}