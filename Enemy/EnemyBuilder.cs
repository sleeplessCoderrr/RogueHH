using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBuilder : EntitiesBuilder
{
    [Header("Data")]
    private EnemyData _enemyData;
    private EnemyConfig _enemyConfig;
    private List<Room> _rooms;

    public EnemyBuilder SetData(EnemyConfig enemyConfig, EnemyData enemyData)
    {
        _rooms = MapManager.Instance.mapData.Rooms;
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
            foreach (var room in _rooms)
            {
                var isValid = false;
                while (!isValid)
                {
                    var x = Random.Range(room.X, room.X + room.Width);
                    var y = Random.Range(room.Y, room.Y + room.Height);
                    if (IsValidPosition(mapData, x, y))
                    {
                        var idx = MapUtility.TakeRandomPrefabs(_enemyConfig.enemyPrefabs);
                        var worldPosition = new Vector3(x*2, 1, y*2);
                        var objectInstance = Object.Instantiate(
                            _enemyConfig.enemyPrefabs[idx], 
                            worldPosition, 
                            Quaternion.identity, 
                            ParentTransform);
                        _enemyData.enemyPosition = new Vector3Int(x*2, 1, y*2);
                        objects.Append(objectInstance);
                        entitiesCount++;
                        isValid = true;
                    }
                }
                
            }
        }
        
        return objects;
    }
}