﻿using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public abstract class MapBaseBuilder
    {
        protected Transform ParentTransform;
        protected GameObject[] Prefabs;
        protected int Width, Height;
        protected List<Room> Rooms;
        protected Tile[,] Grid;
    
        public void SetParent(Transform parent)
        {
            this.ParentTransform = parent;
        }

        public void SetPrefab(GameObject[] prefab)
        {
            this.Prefabs = prefab;
        }
    
        public abstract void InitializeGrid();
    
        public Tile[,] Build()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (Grid[x, y].IsRoom)
                    {
                        var position = new Vector3(x*2, 0, y*2);
                        var tileObject = Object.Instantiate(Prefabs[0], position, Quaternion.identity, ParentTransform);
                    
                        MapUtility.SetTileAttribute(tileObject);
                        Grid[x, y].TileObject = tileObject;
                    }
                    else
                    {
                        var position = new Vector3(x * 2, 0, y * 2);
                        var colliderObject = new GameObject("Obstacle");
                        colliderObject.transform.position = position;
                        colliderObject.transform.parent = ParentTransform;

                        var boxCollider = colliderObject.AddComponent<BoxCollider>();
                        boxCollider.size = new Vector3(2, 0, 2);
                        boxCollider.isTrigger = false;
                        colliderObject.layer = LayerMask.NameToLayer("Environment");
                    }
                }
            }

            return Grid;
        }
    }
}