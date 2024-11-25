using System.Collections.Generic;
using UnityEngine;

public class HighLightTileCommand : ICommand
{
    private Color _highlightColor;
    private Dictionary<GameObject, Color> _originalColors;
    private GameObject _lastHoveredTile;

    public HighLightTileCommand(Color highlightColor)
    {
        _highlightColor = highlightColor;
        _originalColors = new Dictionary<GameObject, Color>();
    }

    public void HighlightTile(GameObject tile)
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

    public void ResetPreviousTile()
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

    public void Execute (GameObject newTile)
    {
        if (newTile != _lastHoveredTile)
        {
            ResetPreviousTile();
            _lastHoveredTile = newTile;
            HighlightTile(newTile);
        }
    }
}