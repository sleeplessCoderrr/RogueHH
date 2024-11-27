using UnityEngine;

public class FloorDecorationBuilder
{
    private Transform _parentTransform;
    private GameObject[] _decorations;
    private Tile[,] _gird;

    public FloorDecorationBuilder SetParent(Transform parent)
    {
        _parentTransform = parent;
        return this;
    }
    
}