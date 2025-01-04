using UnityEngine;

[System.Serializable]
public class Skill
{
    public Sprite icon;         
    public string name;         
    public string description;  
    public int unlockLevel;       
    public float cooldownTime;    
    public float activeTime;      
    public bool IsUnlocked { get; set; }
    public void Unlock(SkillUnlockEventChannel skillUnlockEventChannel)
    {
        if (!IsUnlocked)
        {
            IsUnlocked = true;
            skillUnlockEventChannel?.RaiseEvent(this);
        }
    }
}