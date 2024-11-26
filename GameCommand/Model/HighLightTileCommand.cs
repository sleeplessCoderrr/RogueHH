using System.Collections.Generic;
using UnityEngine;

public class HighLightTileCommand : ICommand
{
    private Dictionary<GameObject, Color> _originalColors;
    private GameObject _lastHoveredTile;
    private Color _highlightColor;
    private GameObject _newTile;

    public HighLightTileCommand(Color highlightColor)
    {
        this._highlightColor = highlightColor;
        this._originalColors = new Dictionary<GameObject, Color>();
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
            this. _lastHoveredTile = _newTile;
            HighlightTile(_newTile);
        }
    }

    private void HighlightTile(GameObject tile)
    {
        if (tile == null) 
            return;

        var renderer = tile.GetComponentInChildren<Renderer>();
        if (renderer == null) 
            return;

        if (!_originalColors.ContainsKey(tile))
        {
            _originalColors[tile] = renderer.material.color;
        }

        renderer.material.color = _highlightColor;
    }


    private void ResetPreviousTile()
    {
        if (_lastHoveredTile == null) 
            return;

        var renderer = _lastHoveredTile.GetComponentInChildren<Renderer>();
        if (renderer == null) 
        {
            _lastHoveredTile = null;
            return;
        }

        if (_originalColors.TryGetValue(_lastHoveredTile, out var originalColor))
        {
            renderer.material.color = originalColor;
        }

        _lastHoveredTile = null;
    }

    
}