using System.Collections.Generic;
using UnityEngine;

public class HighLightTileCommand
{
    private List<Vector2Int> _previousPath;
    private GameObject _lastHoveredTile;
    private List<Vector2Int> _path;
    private Color _highlightColor;
    private Color _originalColor;
    private GameObject _newTile;
    private bool _isInitColor;
    private Tile[,] _tiles;

    public HighLightTileCommand(Color highlightColor)
    {
        _isInitColor = false;
        _highlightColor = highlightColor;
        _previousPath = new List<Vector2Int>();
    }

    public HighLightTileCommand SetTile(Tile[,] tiles)
    {
        _tiles = tiles;
        return this;
    }

    public HighLightTileCommand SetNewTile(GameObject tile)
    {
        _newTile = tile;
        return this;
    }

    public HighLightTileCommand SetPath(List<Vector2Int> path)
    {
        _path = path;
        return this;
    }

    public void Highlight()
    {
        if (!_isInitColor)
        {
            _originalColor = GetNormalColor();
            _isInitColor = true;
        }

        if (_newTile != _lastHoveredTile)
        {
            _lastHoveredTile = _newTile;
            ResetHighlightedTiles();
            HighlightPath();
        }
    }

    private void HighlightPath()
    {
        if (_path == null) return;

        foreach (var position in _path)
        {
            var tile = GetTileFromGrid(position);
            if (tile == null) continue;

            var renderer = tile.GetComponentInChildren<Renderer>();
            renderer.material.color = _highlightColor;
        }

        _previousPath = new List<Vector2Int>(_path);
    }

    private void ResetHighlightedTiles()
    {
        if (_previousPath == null) return;

        foreach (var position in _previousPath)
        {
            var tile = GetTileFromGrid(position);
            if (tile == null) continue;

            var renderer = tile.GetComponentInChildren<Renderer>();
            renderer.material.color = _originalColor;
        }

        _previousPath.Clear();
    }

    private Color GetNormalColor()
    {
        return _newTile.GetComponentInChildren<Renderer>().material.color;
    }

    private GameObject GetTileFromGrid(Vector2Int position)
    {
        return _tiles[position.x, position.y]?.TileObject;
    }
}
