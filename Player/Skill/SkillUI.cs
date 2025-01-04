using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class SkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image skillIcon;
    public Text descriptionText;
    public GameObject hoverOverlay;
    public GameObject lockedOverlay;
    public string skillName; 
    public Skill skill;

    public void Setup(Skill skill)
    {
        this.skill = skill;
        skillName = skill.name;
        skillIcon.sprite = skill.icon;
        descriptionText.text = skill.description;
        lockedOverlay.SetActive(!skill.IsUnlocked);
        hoverOverlay.SetActive(false);
        if (!this.skill.IsUnlocked)
        {
            descriptionText.text = $"Unlocked at level {this.skill.unlockLevel}";
            lockedOverlay.SetActive(true);
        }
        else
        {
            descriptionText.text = this.skill.description;
            lockedOverlay.SetActive(false);
        }
    }

    public void Unlock()
    {
        lockedOverlay.SetActive(false);
        descriptionText.text = skill.description; 
        Debug.Log($"Skill {skillName} unlocked, description updated.");
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOverlay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOverlay.SetActive(false);
    }
}