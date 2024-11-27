using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    
    private HighLightTileCommand _tileHighlighter;
    private List<Vector2Int> _currentPath;
    private Vector2Int _hoveredPosition;
    private Vector3 _playerPosition;
    private GameObject _hoveredTile;
    private Color _hoverColor;
    private bool _isMapInit;
    private Camera _camera;
    private Tile[,] _tiles;

    private void Awake()
    {
        Singleton();
        _isMapInit = false;
        _camera = Camera.main;
        _currentPath = new List<Vector2Int>();
        _hoverColor = new Color(0.4f, 0.4f, 0.4f);
        _tileHighlighter = new HighLightTileCommand(_hoverColor); 
    }
    
    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!_isMapInit)
        {
            _tiles = MapManager.Instance.mapData.MapTileData;
            _tileHighlighter.SetTile(_tiles);
            _isMapInit = true;
        }
        
        GetInitialData();
        if (!_hoveredTile) return;
        
        GetPathData();
        if(_currentPath == null) return;
        
        HandleHover();
    }

    private void HandleHover()
    {
        _tileHighlighter
        .SetNewTile(_hoveredTile)
        .SetPath(_currentPath)
        .Execute(); 
    }

    private void GetPathData()
    {
        _currentPath = MoveUtility.GetPath(
        _tiles, 
        new Vector2Int((int)_playerPosition.x/2, (int)_playerPosition.z/2),
        new Vector2Int((int)_hoveredTile.transform.position.x/2, (int)_hoveredTile.transform.position.z/2));
    }

    private void GetInitialData()
    {
        _playerPosition = MoveUtility.GetPlayerData();
        _hoveredTile = MoveUtility.GetTileFromRaycast(_camera);
    }
}