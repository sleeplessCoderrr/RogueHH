using System;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance; 
    private SkyboxBuilder _skyboxBuilder;

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
        _skyboxBuilder = new SkyboxBuilder()
            .SetColor(Color.black)        
            .SetShader("Unlit/Color")    
            .SetAmbientLight(Color.black) 
            .DisableSun()                
            .Apply();                    
    }
}