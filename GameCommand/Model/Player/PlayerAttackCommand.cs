using UnityEngine;

public class PlayerAttackCommand : ICommand
{
    public CommandType CommandType { get; set; }

    public PlayerAttackCommand()
    {
        CommandType = CommandType.Player;
    }

    public void Execute()
    {
        
    }
}