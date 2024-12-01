using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "SO/Config/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Player Components")]
    public GameObject playerPrefab;
    public Animator animator;
    
    [Header("Player Settings")]
    public float walkSpeed = 2f;
}