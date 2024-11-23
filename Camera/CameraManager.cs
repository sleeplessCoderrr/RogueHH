using System;
using System.Threading.Tasks;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera")]
    private PlayerData _playerData;

    private void Start()
    {
        _playerData = PlayerStateManager.Instance.playerData;
    }

    private void Update()
    {
        _playerData = PlayerStateManager.Instance.playerData;
        transform.position = new Vector3((_playerData.playerPosition.x*2)-10, 10, (_playerData.playerPosition.z*2)-10);
    }
}