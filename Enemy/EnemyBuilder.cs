using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBuilder : EntitiesBuilder
{
    [Header("Data")]
    private EnemyConfig _enemyConfig;
    private List<Room> _rooms;
    private Tile[,] _mapGrid;

    public EnemyBuilder SetData(EnemyConfig enemyConfig)
    {
        _rooms = MapManager.Instance.mapData.Rooms;
        _mapGrid = MapManager.Instance.mapData.MapTileData;
        _enemyConfig = enemyConfig;
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
                        _mapGrid[x, y].IsEnemy= true;
                        
                        var objectInstance = Object.Instantiate(
                            _enemyConfig.enemyPrefabs[idx], 
                            worldPosition, 
                            Quaternion.identity, 
                            ParentTransform);
                        objectInstance.AddComponent<EnemyController>();
                        objectInstance.AddComponent<EnemyStateManager>();
                        
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