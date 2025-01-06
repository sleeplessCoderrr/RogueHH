public class EnemyAttributes
{
    public float Health;
    public float Attack;
    public float Defense;

    public EnemyAttributes(float health, float attack, float defense)
    {
        Health = health;
        Attack = attack;
        Defense = defense;
    }
}

public static class EnemyAttributeScaler
{
    public static EnemyAttributes ScaleAttributes(EnemyAttributes baseAttributes, int level, int maxLevel = 101)
    {
        var healthMultiplier = 5.0f; 
        var attackMultiplier = 3.0f; 
        var defenseMultiplier = 2.0f; 

        var health = ScaleAttribute(baseAttributes.Health, level, maxLevel, healthMultiplier);
        var attack = ScaleAttribute(baseAttributes.Attack, level, maxLevel, attackMultiplier);
        var defense = ScaleAttribute(baseAttributes.Defense, level, maxLevel, defenseMultiplier);

        return new EnemyAttributes(health, attack, defense);
    }

    private static float ScaleAttribute(float baseValue, int level, int maxLevel, float maxMultiplier)
    {
        return baseValue * (1 + (level - 1) / (float)(maxLevel - 1) * (maxMultiplier - 1));
    }
}