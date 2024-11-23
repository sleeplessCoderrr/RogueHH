using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "SO/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Player Components")]
    public Animator animator;
    
    [Header("Player Settings")]
    public float walkSpeed = 5f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    
}