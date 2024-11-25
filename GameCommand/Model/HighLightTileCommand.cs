using System.Collections.Generic;
using UnityEngine;

public class HighLightTileCommand : ICommand
{
    private Dictionary<GameObject, Color> _originalColors;
    private GameObject _lastHoveredTile;
    private Color _highlightColor;
    private GameObject _newTile;

    public HighLightTileCommand SetColor(Color highlightColor)
    {
        this._highlightColor = highlightColor;
        _originalColors = new Dictionary<GameObject, Color>();
        return this;
    }
    
    public HighLightTileCommand SetNewTile(GameObject tile)
    {
        _newTile = tile;
        return this;
    }

    public void Execute ()
    {
        if (_newTile != _lastHoveredTile)
        {
            ResetPreviousTile();
            _lastHoveredTile = _newTile;
            HighlightTile(_newTile);
        }
    }

    private void HighlightTile(GameObject tile)
    {
        if (tile == null) return;

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