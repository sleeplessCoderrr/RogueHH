using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilder : EntitiesBuilder
{
    [Header("Data")]
    private EnemyConfig _enemyConfig;
    private List<Room> _rooms;
    private Tile[,] _mapGrid;

    [Header("Dependencies")]
    private EnemyStateChangeEventChannel _stateChangeEventChannel; 

    public EnemyBuilder SetData(EnemyConfig enemyConfig)
    {
        _rooms = MapManager.Instance.mapData.Rooms;
        _mapGrid = MapManager.Instance.mapData.MapTileData;
        _enemyConfig = enemyConfig;
        _stateChangeEventChannel = EnemyDirector.Instance.stateChangeEventChannel;
        return this;
    }

    public override GameObject[] Build(MapConfig mapConfig, MapData mapData, int count)
    {
        var entitiesCount = 0;
        var objects = new List<GameObject>();

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
                        var worldPosition = new Vector3(x * 2, 1, y * 2);
                        _mapGrid[x, y].IsEnemy = true;

                        var objectInstance = Object.Instantiate(
                            _enemyConfig.enemyPrefabs[0],
                            worldPosition,
                            Quaternion.identity,
                            ParentTransform
                        );

                        var enemyController = objectInstance.AddComponent<EnemyController>();
                        var enemyStateManager = objectInstance.AddComponent<EnemyStateManager>();
                        var stateChangeEventChannel = ScriptableObject.CreateInstance<EnemyStateChangeEventChannel>();
                        enemyStateManager.stateChangeEventChannel = stateChangeEventChannel;

                        objects.Add(objectInstance);
                        entitiesCount++;
                        isValid = true;
                    }
                }
            }
        }

        return objects.ToArray();
    }
}
