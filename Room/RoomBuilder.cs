using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : IBuild
{
    private RoomConfigSO _roomConfig;
    private List<Tile> _roomTiles;
    private GameObject _tile;
    private GameObject[] _floorDecorations;
    private GameObject[] _roomDecorations;

    public RoomBuilder(RoomConfigSO roomConfig, GameObject tile, GameObject[] floorDecorations, GameObject[] roomDecorations)
    {
        this._roomConfig = roomConfig;
        this._tile = tile;
        this._floorDecorations = floorDecorations;
        this._roomDecorations = roomDecorations;
        this._roomTiles = new List<Tile>();
    }
    
    public void Build(Vector3 position)
    {
        GenerateFloor(position);
    }
    
    private void GenerateFloor(Vector3 position)
    {
        var offsetX = (_roomConfig.widthX - 1) * _roomConfig.spacing * 0.5f;
        var offsetZ = (_roomConfig.widthY - 1) * _roomConfig.spacing * 0.5f;

        for (var x = 0; x < _roomConfig.widthX; x++)
        {
            for (var z = 0; z < _roomConfig.widthY; z++)
            {
                var tilePosition = new Vector3(x * _roomConfig.spacing - offsetX, position.y, z * _roomConfig.spacing - offsetZ);
                var tile = new Tile(tilePosition);
                
                _roomTiles.Add(tile);
                
                var tileObject = Object.Instantiate(_tile, tilePosition, Quaternion.identity);
                tileObject.isStatic = true;

                if (Random.value > 0.90f)
                {
                    RandomizeFloor(tilePosition);
                }
            }
        }
    }

    private void RandomizeFloor(Vector3 tilePosition)
    {
        var randomRotation = Random.Range(0, 3) * 90f;
        var randomRotationQuat = Quaternion.Euler(0, randomRotation, 0);

        var randomDecoration = _floorDecorations[Random.Range(0, _floorDecorations.Length)];
        var topDecoration = Object.Instantiate(
            randomDecoration, 
            tilePosition + new Vector3(0, _roomConfig.objectYOffset, 0), 
            randomRotationQuat
        );
        
        topDecoration.isStatic = true;
    }

    public List<Tile> GetRoomTiles()
    {
        return _roomTiles;
    }
}
