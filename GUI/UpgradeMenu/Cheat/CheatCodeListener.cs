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
                playerData.currentExpPoint += 10;
                break;
            case "tpagamegampang":
                playerData.zhen += 10000;
                break;
            case "opensesame":
                for (int i = 1; i <= 101; i++)
                {
                    if (!playerData.playerFloorLevel.Contains(i))
                    {
                        playerData.playerFloorLevel.Add(i);
                    }
                }
                DropdownHandler.Instance.PopulateDropdown();
                break;
        }
    }
}