using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    public static DropdownHandler Instance;

    public Dropdown levelDropdown;
    public LevelSelectionEventChannel levelSelectionEventChannel;
    public PlayerData playerData;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        PopulateDropdown();
        SetDefaultBossLevel();
        SetDropdownToCurrentLevel();
        levelDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    public void PopulateDropdown()
    {
        levelDropdown.ClearOptions();
        foreach (int level in playerData.playerFloorLevel)
        {
            levelDropdown.options.Add(new Dropdown.OptionData("Floor " + level));
        }
        levelDropdown.RefreshShownValue();
    }

    private void SetDefaultBossLevel()
    {
        levelDropdown.options.Add(new Dropdown.OptionData("Boss"));
    }

    private void SetDropdownToCurrentLevel()
    {
        if (playerData.selectedLevel <= playerData.playerFloorLevel.Count)
        {
            levelDropdown.value = playerData.selectedLevel - 1; 
        }
        else
        {
            levelDropdown.value = playerData.playerFloorLevel.Count; 
        }
    }

    private void OnDropdownValueChanged(int index)
    {
        if (index < playerData.playerFloorLevel.Count)
        {
            playerData.selectedLevel = playerData.playerFloorLevel[index];
        }
        else
        {
            playerData.selectedLevel = -1; // -1 means "Boss"
        }

        var selectedLevel = levelDropdown.options[index].text;
        levelSelectionEventChannel.RaiseEvent(selectedLevel);

        Debug.Log("Selected Level: " + playerData.selectedLevel);
    }

    private void OnDestroy()
    {
        levelDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
    }
}
