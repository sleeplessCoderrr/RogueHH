using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : IBuild
{
    private GameObject floorTile;
    private GameObject[] floorDecorations;
    private float spacing;
    private float objectYOffset;
    private int width;
    private int height;

    public RoomBuilder(GameObject floor, GameObject[] decorations, float spacing, float objectYOffset, int width, int height)
    {
        this.floorTile = floor;
        this.floorDecorations = decorations;
        this.spacing = spacing;
        this.objectYOffset = objectYOffset;
        this.width = width;
        this.height = height;
    }
    
    public void Build(Vector3 position, params object[] args)
    {
        GenerateFloor(position);
    }
    
    private void GenerateFloor(Vector3 position)
    {
        float offsetX = (width - 1) * spacing * 0.5f;
        float offsetZ = (height - 1) * spacing * 0.5f;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 tilePosition = new Vector3(x * spacing - offsetX, 0, z * spacing - offsetZ);
                GameObject tile = Object.Instantiate(floorTile, tilePosition, Quaternion.identity);
                tile.isStatic = true;

                if (Random.value > 0.60f && Random.value < 0.70f)
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

        GameObject randomDecoration = floorDecorations[Random.Range(0, floorDecorations.Length)];
        GameObject topDecoration = Object.Instantiate(
            randomDecoration, 
            tilePosition + new Vector3(0, objectYOffset, 0), 
            randomRotationQuat
        );
        
        topDecoration.isStatic = true;
    }
}
