using UnityEngine;

[CreateAssetMenu(fileName = "Critical Damage Item", menuName = "SO/Player Item/Critical Damage Item")]
public class CritcalDamageItem : ScriptableObject
{
    public int baseCriticalDamage = 150;
    public int upgradeValue = 5;
} 