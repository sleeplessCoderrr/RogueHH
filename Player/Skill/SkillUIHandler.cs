using UnityEngine;
using System.Collections.Generic;

public class SkillUIHandler : MonoBehaviour
{
    public SkillUnlockEventChannel skillUnlockEventChannel;
    public Transform skillUIParent; 
    public List<GameObject> skillUIPrefabs; 

    private void OnEnable()
    {
        skillUnlockEventChannel.OnSkillUnlocked += UpdateSkillUI;
    }

    private void OnDisable()
    {
        skillUnlockEventChannel.OnSkillUnlocked -= UpdateSkillUI;
    }

    private void UpdateSkillUI(Skill skill)
    {
        foreach (Transform child in skillUIParent)
        {
            var skillUI = child.GetComponent<SkillUI>();
            if (skillUI != null && skillUI.skillName == skill.name)
            {
                skillUI.Unlock();
                return;
            }
        }

        var prefabToUse = GetSkillPrefab(skill);
        if (prefabToUse == null) return;

        var newSkillUI = Instantiate(prefabToUse, skillUIParent);
        var skillUIComponent = newSkillUI.GetComponent<SkillUI>();
        skillUIComponent.Setup(skill);
    }

    private GameObject GetSkillPrefab(Skill skill)
    {
        foreach (var prefab in skillUIPrefabs)
        {
            var prefabSkillUI = prefab.GetComponent<SkillUI>();
            if (prefabSkillUI != null && prefabSkillUI.skillName == skill.name)
            {
                return prefab;
            }
        }

        return null;
    }
}