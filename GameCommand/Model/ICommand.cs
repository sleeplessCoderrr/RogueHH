using UnityEngine;

public enum CommandType
{
    Enemy,
    Player
}

public interface ICommand
{
    public CommandType CommandType { get; set; }
    public abstract void Execute();
}