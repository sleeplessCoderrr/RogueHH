public class EnemyMoveCommand : ICommand
{
    public CommandType CommandType { get; set; }

    public EnemyMoveCommand()
    {
        CommandType = CommandType.Enemy;
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }
}