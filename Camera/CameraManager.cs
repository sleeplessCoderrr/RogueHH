using System;
using System.Threading.Tasks;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    
    [Header("Camera")]
    private PlayerData _playerData;

    private void Awake()
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

    private void Start()
    {
        _playerData = PlayerStateManager.Instance.playerData;
    }

    private void Update()
    {
        _playerData = PlayerStateManager.Instance.playerData;
        transform.position = new Vector3((_playerData.playerPosition.x)-10, 10, (_playerData.playerPosition.z)-10);
    }
}