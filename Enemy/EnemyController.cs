using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    private Tile[,] _tiles;
    private Vector3 _playerPosition;
    private bool _isActive;

    private void Start()
    {
        _isActive = false;
        _tiles = MapManager.Instance.mapData.MapTileData;
        _playerPosition = PlayerDirector.Instance.playerData.playerPosition;
    }
    
    private void Update()
    {
        CheckPlayer();
        CheckLineOfSight();
    }

    private void CheckPlayer()
    {
        if (!EnemyUtils.CheckPlayerRange(
            8,
            transform,
            _playerPosition)) return;
        _isActive = true;

    }

    private void CheckLineOfSight()
    {
        if (!_isActive) return;
    }
}
