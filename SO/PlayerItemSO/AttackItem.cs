using UnityEngine;

[CreateAssetMenu(fileName = "Attack Item", menuName = "SO/Player Item/Attack Item")]
public class AttackItem : ScriptableObject
{
    public float baseAttack = 5f;
    public float upgradeValue = 2f;
}