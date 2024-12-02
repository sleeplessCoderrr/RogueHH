using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerStateManager _playerStateManager;
    private HighLightTileCommand _tileHighlighter;
    private CommandInvoker _commandInvoker;
    private List<Vector2Int> _currentPath;
    private Vector3 _playerPosition;
    private GameObject _hoveredTile;
    private Player _playerEntity; 
    private Color _hoverColor;
    private bool _isMapInit;
    private Camera _camera;
    private Tile[,] _tiles;

    private async void Start()
    {
        Singleton();
        _isMapInit = false;
        _camera = Camera.main;
        _currentPath = new List<Vector2Int>();
        _commandInvoker = new CommandInvoker();
        _hoverColor = new Color(0.5f, 0.5f, 0.5f);
        _tileHighlighter = new HighLightTileCommand(_hoverColor);

        await Task.Delay(2000); 
        if (PlayerDirector.Instance == null)
        {
            Debug.LogError("PlayerDirector instance is null.");
            return;
        }

        _playerEntity = PlayerDirector.Instance.Player;
        if (_playerEntity == null)
        {
            Debug.LogError("Player entity is null.");
            return;
        }

        _playerStateManager = PlayerStateManager.Instance;
        if (_playerStateManager == null)
        {
            Debug.LogError("PlayerStateManager instance is null.");
            return;
        }

        _playerStateManager.SetPlayerEntity(_playerEntity);
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
        if (_currentPath == null) return;

        HandleHover();

        if (Input.GetMouseButtonDown(0))
        {
            _commandInvoker.AddCommand(new PlayerMoveCommand(_currentPath));
            _commandInvoker.ExecuteCommand();
        }
    }

    private void HandleHover()
    {
        _tileHighlighter.SetNewTile(_hoveredTile).SetPath(_currentPath).Execute();
    }

    private void GetPathData()
    {
        _currentPath = MoveUtility.GetPath(
            _tiles,
            new Vector2Int((int)_playerPosition.x / 2, (int)_playerPosition.z / 2),
            new Vector2Int((int)_hoveredTile.transform.position.x / 2, (int)_hoveredTile.transform.position.z / 2)
        );
    }

    private void GetInitialData()
    {
        _playerPosition = MoveUtility.GetPlayerData();
        _hoveredTile = MoveUtility.GetTileFromRaycast(_camera);
    }
}
