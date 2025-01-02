using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public bool isPlayerMoving;
    public bool isCanceled; 
    
    private PlayerStateManager _playerStateManager;
    private HighLightTileCommand _tileHighlighter;
    private CommandInvoker _commandInvoker;
    private List<Vector2Int> _currentPath;
    private GameObject _objectFromRayCast;
    private Vector3 _playerPosition;
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
        
        isPlayerMoving = false;
        isCanceled = false;
        
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
        if (!_objectFromRayCast) return;

        GetPathData();
        if (_currentPath == null) return;

        HandleHover();

        if (Input.GetMouseButtonDown(0) 
            && !(_objectFromRayCast.tag == "Tile"))
        {
            if (isPlayerMoving)
            {
                isCanceled = true;
                return;
            }
            
            _commandInvoker.AddCommand(new PlayerAttackCommand());
            _commandInvoker.ExecuteCommand();
        }
        
        if (Input.GetMouseButtonDown(0)
            && !MoveUtility.IsEnemy(
            _tiles, 
            (int)_objectFromRayCast.transform.position.x/2, 
            (int)_objectFromRayCast.transform.position.z/2
            ))
        {
            if (isPlayerMoving)
            {
                isCanceled = true;
                return;
            }
            
            _commandInvoker.AddCommand(new PlayerMoveCommand(_currentPath));
            _commandInvoker.ExecuteCommand();
        }
    }

    private void HandleHover()
    {
        if (!(_objectFromRayCast.tag == "Tile")) return;
        _tileHighlighter
        .SetNewTile(_objectFromRayCast)
        .SetPath(_currentPath)
        .Execute();
    }

    private void GetPathData()
    {   
        if(!MoveUtility.IsValidMove(_tiles, (int)_playerPosition.x / 2, (int)_playerPosition.z / 2))return;
        _currentPath = MoveUtility.GetPath(
            _tiles,
            new Vector2Int((int)_playerPosition.x / 2, (int)_playerPosition.z / 2),
            new Vector2Int((int)_objectFromRayCast.transform.position.x / 2, (int)_objectFromRayCast.transform.position.z / 2)
        );
    }

    private void GetInitialData()
    {
        _playerPosition = MoveUtility.GetPlayerData();
        _objectFromRayCast = MoveUtility.GetObjectFromRayCast(_camera);
    }
}
