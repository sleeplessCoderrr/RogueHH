using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Defense Item", menuName = "SO/Player Item/Defense Item")]
public class DefenseItem : ScriptableObject
{
    public float baseDefense = 5f;
    public float upgradeValue = 5f;
}