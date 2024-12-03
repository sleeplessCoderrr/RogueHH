using UnityEngine;

[CreateAssetMenu(fileName = "Health Item", menuName = "SO/Player Item/Health Item")]
public class HealthItem : ScriptableObject
{
    public float baseHealth = 20f;
    public float upgradeValue = 10f;
}