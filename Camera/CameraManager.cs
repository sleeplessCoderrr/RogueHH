using System;
using System.Threading.Tasks;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    
    [Header("Camera")]
    private PlayerData _playerData;
    private Vector3 _velocity = Vector3.zero;

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

    private async void Start()
    {   
        await Task.Delay(1000);
        _playerData = PlayerDirector.Instance.playerData;
    }

    private void Update()
    {
        _playerData = PlayerDirector.Instance.playerData;
        var targetPosition = new Vector3(
            (_playerData.playerPosition.x) - 10,
            10,
            (_playerData.playerPosition.z) - 10
        );

        transform.position = Vector3.SmoothDamp(
            transform.position, 
            targetPosition, 
            ref _velocity, 
            0.5f
        );
    }
}