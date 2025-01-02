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
        levelDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    public void PopulateDropdown()
    {
        levelDropdown.ClearOptions();
        foreach (int level in playerData.playerLevel)
        {
            levelDropdown.options.Add(new Dropdown.OptionData("Floor " + level));
        }

        levelDropdown.RefreshShownValue();
    }

    private void SetDefaultBossLevel()
    {
        levelDropdown.options.Add(new Dropdown.OptionData("Boss"));
    }

    private void OnDropdownValueChanged(int index)
    {
        var selectedLevel = levelDropdown.options[index].text;
        levelSelectionEventChannel.RaiseEvent(selectedLevel);
    }

    private void OnDestroy()
    {
        levelDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
    }
}