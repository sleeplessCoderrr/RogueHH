using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera _camera;
    private HighLightTileCommand _tileHighlighter;
    private Vector3 _playerData;

    private void Awake()
    {
        _camera = Camera.main;
        _tileHighlighter = new HighLightTileCommand(); 
    }

    private void Update()
    {
        GetPlayerData();
        var hoveredTile = GetTileFromRaycast();
        
        //Hover Highlight
        _tileHighlighter
        .SetColor(new Color(0.4f, 0.4f, 0.4f))
        .SetNewTile(hoveredTile);
        _tileHighlighter.Execute(); 
    }

    private void GetPlayerData()
    {
        this._playerData = PlayerStateManager.Instance.playerData.playerPosition;
        return;
    }

    private GameObject GetTileFromRaycast()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            return hit.collider?.gameObject;
        }
        return null;
    }
}