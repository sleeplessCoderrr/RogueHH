using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [Header("Assets")]
    public GameObject floorTile;
    public GameObject[] floorDecorations;
    
    [Header("Attribute")]
    [SerializeField]private int _width = 10;
    [SerializeField]private int _height = 10;
    [SerializeField]private float _spacing = 2f;
    [SerializeField]private float _objectYOffset = 2f;
    void Start()
    {
        GenerateFloor();
    }

    private void GenerateFloor()
    {
        float offsetX = (_width - 1) * _spacing * 0.5f;
        float offsetZ = (_height - 1) * _spacing * 0.5f;

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                Vector3 tilePosition = new Vector3(x * _spacing - offsetX, 0, z * _spacing - offsetZ);
                GameObject tile = Instantiate(floorTile, tilePosition, Quaternion.identity, transform);
                tile.isStatic = true;
                if (Random.value > 0.60f && Random.value < 0.70f)
                {
                    randomizesFloor(tilePosition);
                }
            }
        }

        return;
    }

    private void randomizesFloor(Vector3 tilePosition)
    {
        float randomRotation = Random.Range(0, 3) * 90f;
        Quaternion randomRotationQuat = Quaternion.Euler(0, randomRotation, 0);
        
        GameObject randomDecoration = floorDecorations[Random.Range(0, floorDecorations.Length)];
        GameObject topDecoration = Instantiate(
            randomDecoration, 
            tilePosition + new Vector3(0, _objectYOffset, 0), 
            randomRotationQuat, 
            transform);
        
        topDecoration.isStatic = true;
        
        return;
    }
}
