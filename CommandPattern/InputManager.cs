using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera _camera;
    private Color _highlightColor; 
    private GameObject _lastHoveredTile;
    private Dictionary<GameObject, Color> _originalColors;

    private void Awake()
    {
        _camera = Camera.main;
        _highlightColor = new Color(0.4f, 0.4f, 0.4f);
        _originalColors = new Dictionary<GameObject, Color>();
    }

    private void Update()
    {
        var currentHoveredTile = GetTileFromRaycast();

        if (currentHoveredTile != _lastHoveredTile)
        {
            ResetPreviousTile();
            _lastHoveredTile = currentHoveredTile;
            if (currentHoveredTile != null) 
            {
                HighlightTile(currentHoveredTile);
            }
        }
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

    private void HighlightTile(GameObject tile)
    {
        var renderer = tile.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            if (!_originalColors.ContainsKey(tile))
            {
                _originalColors[tile] = renderer.material.color;
            }

            renderer.material.color = _highlightColor;
        }
    }

    private void ResetPreviousTile()
    {
        if (_lastHoveredTile == null) return;

        var renderer = _lastHoveredTile.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            if (_originalColors.TryGetValue(_lastHoveredTile, out var originalColor))
            {
                renderer.material.color = originalColor;
            }
        }

        _lastHoveredTile = null;
    }
}