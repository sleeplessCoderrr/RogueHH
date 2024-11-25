using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera _camera;
    private HighLightTileCommand _tileHighlighter;

    private void Awake()
    {
        _camera = Camera.main; 
        _tileHighlighter = new HighLightTileCommand(); 
    }

    private void Update()
    {
        var hoveredTile = GetTileFromRaycast();
        _tileHighlighter = new HighLightTileCommand()
            .SetColor(new Color(0.4f, 0.4f, 0.4f))
            .SetNewTile(hoveredTile);
        
        _tileHighlighter.Execute(); 
    }

    private GameObject GetTileFromRaycast()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider?.gameObject;
        }
        return null;
    }
}