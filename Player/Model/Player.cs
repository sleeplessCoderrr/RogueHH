using UnityEngine;

public class Player
{
    [Header("Player Config")]
    public PlayerConfig PlayerConfig;
    
    [Header("Player Data")]
    public PlayerData PlayerData;

    public Player(PlayerConfig playerConfig, PlayerData playerData)
    {
        this.PlayerConfig = playerConfig;
        this.PlayerData = playerData;
    }
}
