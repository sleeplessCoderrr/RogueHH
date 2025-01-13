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

        if (InputManager.Instance.isOneHit)
        {
            _enemyData.currentHealth = 0;
            InputManager.Instance.isOneHit = false;
            InputManager.Instance.oneHitCount--;
        }
        else
        {
            _enemyData.currentHealth -= damage;
        }

        if (InputManager.Instance.isLifeSteel)
        {
            if (Random.Range(0f, 1f) <= 0.2f)
            {
                var additionHealth = damage * 0.2f;
                if(_playerData.currentExpPoint < _playerData.maxHealth) _playerData.currentHealth += additionHealth;
            }
        }
        
        _playerStateManager.SetState(PlayerState.Idle);
        PlayerDirector.Instance.playerData.isPlayerTurn = false;
        
        if (InputManager.Instance.isLifeSteel)
        {
            InputManager.Instance.lifeSteelCount--;
            if (InputManager.Instance.lifeSteelCount == 0) InputManager.Instance.isLifeSteel = false;
        }
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