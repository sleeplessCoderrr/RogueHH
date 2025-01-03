using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SkillUnlockEventChannel", menuName = "SO/EventChannel/SkillUnlockEventChannel")]
public class SkillUnlockEventChannel : ScriptableObject
{
    public UnityAction<Skill> OnSkillUnlocked;

    public void RaiseEvent(Skill skill)
    {
        if (OnSkillUnlocked != null)
            OnSkillUnlocked.Invoke(skill);
    }
}