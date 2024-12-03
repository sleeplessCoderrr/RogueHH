using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Critical Rate Item", menuName = "SO/Player Item/Critical Rate Item")]
public class CriticalRateItem : ScriptableObject
{
    public int baseCriticalRate = 5;
    public int upgradeValue = 2;
}