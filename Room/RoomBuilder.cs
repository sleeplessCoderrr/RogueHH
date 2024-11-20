using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : IBuild
{
    private RoomConfig _roomConfig;

    public RoomBuilder(RoomConfig roomConfig)
    {
        this._roomConfig = roomConfig;
    }
    
    public void Build()
    {
        GenerateFloor();
    }

    public void Reset()
    {
        
    }
    
    private void GenerateFloor()
    {
        float offsetX = (_roomConfig.widthX - 1) * _roomConfig.spacing * 0.5f;
        float offsetZ = (_roomConfig.widthY - 1) * _roomConfig.spacing * 0.5f;

        for (int x = 0; x < _roomConfig.widthX; x++)
        {
            for (int z = 0; z < _roomConfig.widthY; z++)
            {
                Vector3 tilePosition = new Vector3(x * _roomConfig.spacing - offsetX, 0, z * _roomConfig.spacing - offsetZ);
                GameObject tile = Object.Instantiate(_roomConfig.floorTile, tilePosition, Quaternion.identity);
                tile.isStatic = true;

                if (Random.value > 0.90f)
                {
                    RandomizeFloor(tilePosition);
                }
            }
        }
    }

    private void RandomizeFloor(Vector3 tilePosition)
    {
        float randomRotation = Random.Range(0, 3) * 90f;
        Quaternion randomRotationQuat = Quaternion.Euler(0, randomRotation, 0);

        GameObject randomDecoration = _roomConfig.floorDecorations[Random.Range(0, _roomConfig.floorDecorations.Length)];
        GameObject topDecoration = Object.Instantiate(
            randomDecoration, 
            tilePosition + new Vector3(0, _roomConfig.objectYOffset, 0), 
            randomRotationQuat
        );
        
        topDecoration.isStatic = true;
    }
}
