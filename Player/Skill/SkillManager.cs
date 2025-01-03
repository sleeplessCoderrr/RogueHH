using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerLevelUpdateEventChannel playerLevelUpdateEventChannel;
    public SkillUnlockEventChannel skillUnlockEventChannel;
    public List<Skill> skills = new List<Skill>();

    private void OnEnable()
    {
        playerLevelUpdateEventChannel.OnPlayerLevelUpdated += CheckUnlockableSkills;
    }

    private void OnDisable()
    {
        playerLevelUpdateEventChannel.OnPlayerLevelUpdated -= CheckUnlockableSkills;
    }

    private void Start()
    {
        CheckUnlockableSkills(playerData.PlayerLevel);
    }

    private void CheckUnlockableSkills(int playerLevel)
    {
        foreach (var skill in skills)
        {
            if (!skill.IsUnlocked && playerLevel >= skill.unlockLevel)
            {
                skill.Unlock(skillUnlockEventChannel);
            }
        }
    }
}