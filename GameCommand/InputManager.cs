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
    private bool _isMapInit;
    private Camera _camera;
    private Tile[,] _tiles;

    private void Awake()
    {
        Singleton();
        _isMapInit = false;
        _camera = Camera.main;
        _currentPath = new List<Vector2Int>();
        _tileHighlighter = new HighLightTileCommand(new Color(0.4f, 0.4f, 0.4f)); 
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
            _isMapInit = true;
        }
        
        GetInitialData();
        if (!_hoveredTile) return;
        
        GetPathData();
        if (_currentPath == null)
        {
            Debug.Log("Eror pathfinding");
            return;
        }
        
        HandleHover();
    }

    private void HandleHover()
    {
        _tileHighlighter
        .SetNewTile(_hoveredTile);
        _tileHighlighter.Execute(); 
    }

    private void GetPathData()
    {
        _currentPath = MoveUtility.GetPath(
        _tiles, 
        new Vector2Int((int)_playerPosition.x, (int)_playerPosition.z),
        new Vector2Int((int)_hoveredTile.transform.position.x, (int)_hoveredTile.transform.position.z));
    }

    private void GetInitialData()
    {
        _playerPosition = MoveUtility.GetPlayerData();
        _hoveredTile = MoveUtility.GetTileFromRaycast(_camera);
    }
}