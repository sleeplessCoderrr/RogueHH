using System.Collections.Generic;
using UnityEngine;

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
        var objects = new List<GameObject>();
        var nameList = EnemyDirector.Instance.enemyConfig.initials;
        var initializeName = new HashSet<string>(); 
        while (entitiesCount < count)
        {
            foreach (var room in _rooms)
            {
                var isValid = false;
                while (!isValid)
                {
                    var x = Random.Range(room.X, room.X + room.Width);
                    var y = Random.Range(room.Y, room.Y + room.Height);
                    var randomName = nameList[Random.Range(0, nameList.Length)];

                    if (IsValidPosition(mapData, x, y) && !initializeName.Contains(randomName))
                    {
                        var idx = MapUtility.TakeRandomEnemy(
                            _enemyConfig.enemyPrefabs,
                            PlayerDirector.Instance.playerData.selectedLevel
                        );
                        
                        var worldPosition = new Vector3(x * 2, 1, y * 2);
                        _mapGrid[x, y].IsEnemy = true;

                        var objectInstance = InstantiateEnemies(
                            _enemyConfig.enemyPrefabs[idx],
                            worldPosition,
                            ParentTransform
                        );

                        var controller = objectInstance.AddComponent<EnemyController>();
                        var enemyStateManager = objectInstance.AddComponent<EnemyStateManager>();
                        var enemyRaycast = objectInstance.AddComponent<EnemyLineOfSight>();

                        enemyRaycast.player = PlayerDirector.Instance.Player.PlayerInstance.transform;
                        enemyRaycast.obstacleMask = LayerMask.GetMask("Environment", "Player");

                        var canvas = objectInstance.gameObject.GetComponentInChildren<Canvas>();
                        var stateText = canvas.gameObject.GetComponent<StateText>();
                        var infoDisplay = canvas.gameObject.GetComponent<InfoDisplay>();

                        controller.currentText = stateText;
                        controller.infoDisplay = infoDisplay;
                        infoDisplay.SetName(randomName, idx);

                        initializeName.Add(randomName);
                        objects.Add(objectInstance);
                        entitiesCount++;
                        isValid = true;
                    }
                }
            }
        }

        return objects.ToArray();
    }

    private GameObject InstantiateEnemies(GameObject enemyPrefabs, Vector3 position, Transform parentTransform)
    {
        return Object.Instantiate(
            enemyPrefabs,
            position,
            Quaternion.identity,
            parentTransform
        );
    }
}
