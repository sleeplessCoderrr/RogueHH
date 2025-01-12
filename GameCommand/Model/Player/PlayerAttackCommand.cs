using UnityEngine;

public class PlayerAttackCommand : ICommand
{
    public CommandType CommandType { get; set; }
    
    private readonly PlayerStateManager _playerStateManager;
    private readonly PlayerData _playerData;
    private readonly EnemyData _enemyData;

    public PlayerAttackCommand(PlayerStateManager playerStateManager, EnemyData enemyData)
    {
        _playerData = PlayerDirector.Instance.playerData;
        _playerStateManager = playerStateManager;
        CommandType = CommandType.Player;
        _enemyData = enemyData;
    }

    public void Execute()
    {
        _playerStateManager.SetState(PlayerState.Attack);

        var damage = CalculateDamageOutput(
            _playerData.attack, 
            _enemyData.defense,
            _playerData.critRate,
            _playerData.critDamage
        );

        Debug.Log(damage);
        _enemyData.currentHealth -= damage;
        
        _playerStateManager.SetState(PlayerState.Idle);
        PlayerDirector.Instance.playerData.isPlayerTurn = false;
    }
    
    private static float CalculateDamageOutput(
        float attack, 
        float defense, 
        float criticalChance, 
        float criticalDamageMultiplier, 
        float randomFactorRange = 0.1f)
    {
        criticalChance /= 100f;
        criticalDamageMultiplier /= 100f;
            
        var defenseScalingFactor = Random.Range(20, 50);
        var defenseFactor = 1 - (defense / (defense + defenseScalingFactor));
        var baseDamage = attack * defenseFactor;
       
        var isCriticalHit = Random.Range(0f, 1f) <= criticalChance;
        if (isCriticalHit)
        {
            CameraManager.Instance.TriggerShake(0.2f, 0.2f);
            baseDamage *= criticalDamageMultiplier;
        }
        var randomFactor = UnityEngine.Random.Range(1 - randomFactorRange, 1 + randomFactorRange);
        
        EnemyDamage.Instance.TakeDamage((int)(baseDamage * randomFactor), isCriticalHit);
        Debug.Log("kena");
        return baseDamage * randomFactor;;
    }
}