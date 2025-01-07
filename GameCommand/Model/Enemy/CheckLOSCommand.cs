using UnityEngine;

public class CheckLOSCommand : ICommand
{
    private GameObject _enemy;
    private EnemyLineOfSight _losCommand;
    public CommandType CommandType { get; set; }
    
    public CheckLOSCommand(GameObject enemy)
    {
        _enemy = enemy;
        CommandType = CommandType.Enemy;
        _losCommand = _enemy.GetComponent<EnemyLineOfSight>();
    }

    public void Execute()
    {
        if(!_losCommand.activate) _losCommand.activate = true;
        if(PlayerDirector.Instance.playerData.isPlayerTurn) return;
        var result = _losCommand.GetResult();
        Debug.Log(result);
        PlayerDirector.Instance.playerData.isPlayerTurn = true;
    }
}