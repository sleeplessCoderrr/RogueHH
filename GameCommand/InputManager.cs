using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public bool isPaused;
    public bool isCanceled; 
    public bool isEnemyNearby;
    public bool isPlayerMoving;
    public static InputManager Instance;
    public CommandInvoker CommandInvoker;

    public AudioClip dungeonSound;
    public AudioClip attackSound;
    private bool _isPlayingAttackMusic;

    public bool isOneHit;
    public int oneHitCount;
    
    public bool isLifeSteel;
    public int lifeSteelCount;
    
    private PlayerStateManager _playerStateManager;
    private HighLightTileCommand _tileHighlighter;
    private List<Vector2Int> _currentPath;
    private GameObject _objectFromRayCast;
    private Vector3 _playerPosition;
    private PlayerData _playerData;
    private Player _playerEntity; 
    private Color _hoverColor;
    private bool _isMapInit;
    private Camera _camera;
    private Tile[,] _tiles;

    private async void Start()
    {
        Singleton();
        _camera = Camera.main;

        isOneHit = false;
        oneHitCount = 0;
        
        isLifeSteel = false;
        lifeSteelCount = 0;
        
        isPaused = false;
        _isMapInit = false;
        isCanceled = false;
        isEnemyNearby = false;
        isPlayerMoving = false;
        
        _currentPath = new List<Vector2Int>();
        CommandInvoker = new CommandInvoker();
        _hoverColor = new Color(0.5f, 0.5f, 0.5f);
        _tileHighlighter = new HighLightTileCommand(_hoverColor);

        await Task.Delay(2000); 
        _playerEntity = PlayerDirector.Instance.Player;
        _playerData = PlayerDirector.Instance.playerData;
        _playerStateManager = PlayerStateManager.Instance;
        if (_playerStateManager == null || _playerEntity == null) return; 

        _playerStateManager.SetPlayerEntity(_playerEntity);
    }


    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (isPaused) return;
        
        //##TODO: Map Preparation
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

        //##TODO: Input
        HandleMusic();
        HandleHover();

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isOneHit = true;
            oneHitCount = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isLifeSteel = true;
            lifeSteelCount = 4;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _playerData.isPlayerTurn = false;
            return;
        }
        
        if (Input.GetMouseButtonDown(0) && isPlayerMoving)
        {
            isCanceled = true;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if (Physics.Raycast(ray, out var hit))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("Enemy")
                    && Vector3.Distance(hit.collider.gameObject.transform.position,
                        _playerEntity.PlayerInstance.transform.position) <= 3f)
                {
                    _playerData.isPlayerTurn = true;
                    isPlayerMoving = false;
                    var enemyData = hit.collider.gameObject.GetComponent<EnemyController>().enemyData;
                        
                    CommandInvoker.AddCommand(new PlayerAttackCommand(_playerStateManager, enemyData));
                    CommandInvoker.ExecuteCommand();
                    return;
                }

                if (hit.collider.CompareTag("Tile"))
                {
                    _playerData.isPlayerTurn = true;
                    CommandInvoker.AddCommand(new PlayerMoveCommand(_currentPath));
                    CommandInvoker.ExecuteCommand();
                    return;
                }
            }
        }
    }

    private void CheckSkillCount()
    {
        Debug.Log("Life Steel: " + lifeSteelCount);
        Debug.Log("OneHit: " + oneHitCount);
    }

    private void HandleHover()
    {
        _tileHighlighter
        .SetNewTile(_objectFromRayCast)
        .SetPath(_currentPath)
        .Highlight();
    }

    private void GetPathData()
    {   
        if(!MoveUtility.IsValidMove(_tiles, (int)_objectFromRayCast.transform.position.x / 2, (int)_objectFromRayCast.transform.position.z / 2))return;
        _currentPath = MoveUtility.GetPath(
            _tiles,
            new Vector2Int((int)_playerPosition.x / 2, (int)_playerPosition.z / 2),
            new Vector2Int((int)_objectFromRayCast.transform.position.x / 2, (int)_objectFromRayCast.transform.position.z / 2)
        );
        
        if(_currentPath == null) return;
        if(_currentPath.Count <= 0) return;
        _currentPath.RemoveAt(0);
    }
    
    private void GetInitialData()
    {
        _playerPosition = MoveUtility.GetPlayerData();
        _objectFromRayCast = MoveUtility.GetObjectFromRayCast(_camera);
        isEnemyNearby = MapUtility.IsEnemyInRange(
            transform.position, 
            10f,
            EnemyDirector.Instance.EnemyList
        );
    }
    
    private void HandleMusic()
    {
        if (isEnemyNearby && !_isPlayingAttackMusic)
        {
            GameBgmManager.Instance.ChangeAudioClip(attackSound);
            _isPlayingAttackMusic = true;
        }
        else if (!isEnemyNearby && _isPlayingAttackMusic)
        {
            GameBgmManager.Instance.ChangeAudioClip(dungeonSound);
            _isPlayingAttackMusic = false;
        }
    }
}
