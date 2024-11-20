using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : IBuild
{
    private Room _roomAttribute;
    private float _spacing;
    private float _objectYOffset;
    private int _widthX;
    private int _widthY;

    public RoomBuilder(GameObject floor, GameObject[] floorDecorations, GameObject[] roomDecorations, float spacing, float objectYOffset, int widthX, int widthY)
    {
        this._roomAttribute = new Room(floor, floorDecorations, roomDecorations);   
        this._spacing = spacing;
        this._objectYOffset = objectYOffset;
        this._widthX = widthX;
        this._widthY = widthY;
    }
    
    public void Build(Vector3 position, params object[] args)
    {
        GenerateFloor(position);
    }
    
    private void GenerateFloor(Vector3 position)
    {
        float offsetX = (_widthX - 1) * _spacing * 0.5f;
        float offsetZ = (_widthY - 1) * _spacing * 0.5f;

        for (int x = 0; x < _widthX; x++)
        {
            for (int z = 0; z < _widthY; z++)
            {
                Vector3 tilePosition = new Vector3(x * _spacing - offsetX, 0, z * _spacing - offsetZ);
                GameObject tile = Object.Instantiate(_roomAttribute._tiles, tilePosition, Quaternion.identity);
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

        GameObject randomDecoration = _roomAttribute._floorDecorations[Random.Range(0, _roomAttribute._floorDecorations.Length)];
        GameObject topDecoration = Object.Instantiate(
            randomDecoration, 
            tilePosition + new Vector3(0, _objectYOffset, 0), 
            randomRotationQuat
        );
        
        topDecoration.isStatic = true;
    }
}
