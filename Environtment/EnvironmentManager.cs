﻿using System;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance; 
        
    private SkyboxBuilder _skyboxBuilder;
    private Light _playerLight;
    private GameObject _player;
    
    private async void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        _skyboxBuilder = new SkyboxBuilder()
            .SetColor(Color.black)        
            .SetShader("Unlit/Color")    
            .SetAmbientLight(Color.black) 
            .DisableSun()                
            .Apply();

        await Task.Delay(1500);
        _player = PlayerDirector.Instance.Player.PlayerInstance;
        _playerLight = new PlayerLightBuilder()
            .SetType(LightType.Point)
            .SetColor(new Color(1f, 0.8f, 0.6f)) 
            .SetIntensity(2.5f)
            .SetRange(10f)
            .SetParent(_player.transform) 
            .SetShadow()
            .SetLocalPosition(new Vector3(0, 10f, 0)) 
            .Build();
    }

    private void Update()
    {
        if (_playerLight != null)
        {
            _playerLight.transform.position = new Vector3(_player.transform.position.x, 4, _player.transform.position.z);
        }
    }
}