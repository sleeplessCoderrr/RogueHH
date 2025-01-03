using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Image skillIcon;
    public Text skillNameText;
    public Text descriptionText;
    public GameObject lockedOverlay;

    public string skillName; 

    public void Setup(Skill skill)
    {
        skillName = skill.name;
        skillIcon.sprite = skill.icon;
        skillNameText.text = skill.name;
        descriptionText.text = skill.description;
        lockedOverlay.SetActive(!skill.IsUnlocked);
    }

    public void Unlock()
    {
        lockedOverlay.SetActive(false);
    }
}