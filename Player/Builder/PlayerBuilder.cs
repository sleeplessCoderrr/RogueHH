using UnityEngine;

public class PlayerBuilder
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

    private bool IsValidPosition(MapData mapData, int x, int y)
    {
        return mapData.MapTileData[x, y].IsRoom;
    }

    public PlayerBuilder InitializeRandomPosition(MapConfig mapConfig, MapData mapData)
    {
        var isNotValid = false;
        while (!isNotValid)
        {
            var x = Random.Range(0, mapConfig.width - 1);
            var y = Random.Range(0, mapConfig.height - 1);
            if (IsValidPosition(mapData, x, y))
            {
                    var worldPosition = new Vector3(x*2, 1, y*2);
                    GameObject.Instantiate(_playerConfig.playerPrefab, worldPosition, Quaternion.identity);
                    _playerData.playerPosition = new Vector3Int(x, 1, y);
                    Debug.Log(_playerData.playerPosition);
                    isNotValid = true;
                    return this;
            }
        }
        
        return this;
    }
}