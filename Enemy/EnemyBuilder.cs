using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyBuilder : EntitiesBuilder
{
    [Header("Data")]
    private EnemyData _enemyData;
    private EnemyConfig _enemyConfig;

    public EnemyBuilder SetData(EnemyConfig enemyConfig, EnemyData enemyData)
    {
        _enemyConfig = enemyConfig;
        _enemyData = enemyData;
        return this;
    }
    
    public override GameObject[] Build(MapConfig mapConfig, MapData mapData, int count)
    {
        var entitiesCount = 0;
        var objects = new GameObject[count];
        while (entitiesCount < count)
        {
            var x = Random.Range(0, mapConfig.width - 1);
            var y = Random.Range(0, mapConfig.height - 1);
            if (IsValidPosition(mapData, x, y))
            {
                entitiesCount++;
                var idx = MapUtility.TakeRandomPrefabs(_enemyConfig.enemyPrefabs);
                var worldPosition = new Vector3(x*2, 1, y*2);
                var objectInstance = Object.Instantiate(
                    _enemyConfig.enemyPrefabs[idx], 
                    worldPosition, 
                    Quaternion.identity, 
                    ParentTransform);
                _enemyData.enemyPosition = new Vector3Int(x*2, 1, y*2);
                objects.Append(objectInstance);
            }
        }
        
        return objects;
    }
}