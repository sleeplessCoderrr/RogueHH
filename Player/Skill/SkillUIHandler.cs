using UnityEngine;
using UnityEngine.UI;

public class SkillUIHandler : MonoBehaviour
{
    public SkillUnlockEventChannel skillUnlockEventChannel;
    public Transform skillUIParent; 
    public GameObject skillUIPrefab; 

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
            SkillUI skillUI = child.GetComponent<SkillUI>();
            if (skillUI != null && skillUI.skillName == skill.name)
            {
                skillUI.Unlock();
                return; 
            }
        }

        var newSkillUI = Instantiate(skillUIPrefab, skillUIParent);
        var skillUIComponent = newSkillUI.GetComponent<SkillUI>();
        skillUIComponent.Setup(skill);
    }
}