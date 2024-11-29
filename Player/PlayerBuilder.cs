using UnityEngine;

public class PlayerBuilder : EntitiesBuilder
{
    [Header("Data")]
    private PlayerData _playerData;
    private PlayerConfig _playerConfig;
    
    public PlayerBuilder SetData(PlayerConfig playerConfig, PlayerData playerData)
    {
        _playerConfig = playerConfig;
        _playerData = playerData;
        return this;
    }

    public override GameObject[] Build(MapConfig mapConfig, MapData mapData, int count)
    {
        var objects = new GameObject[count];
        while (true)
        {
            var x = Random.Range(0, mapConfig.width - 1);
            var y = Random.Range(0, mapConfig.height - 1);
            if (IsValidPosition(mapData, x, y))
            {
                var worldPosition = new Vector3(x*2, 1, y*2);
                objects[0] = Object.Instantiate(_playerConfig.playerPrefab, worldPosition, Quaternion.identity, ParentTransform);
                _playerData.playerPosition = new Vector3Int(x*2, 1, y*2);
                return objects;
            }
        }
    }
}