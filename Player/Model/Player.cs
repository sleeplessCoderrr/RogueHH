using UnityEngine;

public class Player
{
    [Header("Player Config")]
    public PlayerConfig PlayerConfig;
    
    [Header("Player Data")]
    public PlayerData PlayerData;
    
    public Animator Animator;

    public Player(PlayerConfig playerConfig, PlayerData playerData, Animator animator)
    {
        this.PlayerConfig = playerConfig;
        this.PlayerData = playerData;
        this.Animator = animator;
    }
}
