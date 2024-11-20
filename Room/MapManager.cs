using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;

    private static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapManager>();
                if (_instance == null)
                {
                    GameObject mapManager = new GameObject("MapManager");
                    _instance = mapManager.AddComponent<MapManager>();
                }
            }
            return _instance;
        }
    }

    private IBuild _roomBuilder;
    public GameObject floorTile;
    public GameObject[] floorDecorations;
    public GameObject[] roomDecorations;
    
    [Header("Room Settings")]
    public float spacing = 2f;
    public float objectYOffset = 2f;
    public int roomWidth = 10;
    public int roomHeight = 10;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        _roomBuilder = new RoomBuilder(floorTile, floorDecorations, roomDecorations, spacing, objectYOffset, roomWidth, roomHeight);
    }

    private void Start()
    {
        GenerateRoom(new Vector3(0, 0, 0));
    }

    private void GenerateRoom(Vector3 position)
    {
        _roomBuilder.Build(position);
    }
}
