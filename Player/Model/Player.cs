using UnityEngine;

public class Player
{
    [Header("Player Config")]
    public PlayerConfig PlayerConfig;
    
    [Header("Player Stats")]
    public Vector2 PlayerPosition;

    public Player(PlayerConfig playerConfig, Vector2 playerPosition)
    {
        this.PlayerConfig = playerConfig;
        this.PlayerPosition = playerPosition;
    }
}
