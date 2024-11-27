using UnityEngine;

public class SkyboxBuilder
{
    private Material _skyboxMaterial;

    public SkyboxBuilder()
    {
        _skyboxMaterial = new Material(Shader.Find("Unlit/Color"));
    }

    public SkyboxBuilder SetColor(Color color)
    {
        _skyboxMaterial.color = color;
        return this; 
    }

    public SkyboxBuilder SetShader(string shaderName)
    {
        Shader shader = Shader.Find(shaderName);
        if (shader != null)
        {
            _skyboxMaterial.shader = shader;
        }
        else
        {
            Debug.LogWarning("Shader not found: " + shaderName);
        }
        return this;
    }

    public SkyboxBuilder Apply()
    {
        RenderSettings.skybox = _skyboxMaterial;
        return this;
    }

    public SkyboxBuilder SetAmbientLight(Color color)
    {
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = color;
        return this;
    }

    public SkyboxBuilder DisableSun()
    {
        RenderSettings.sun = null;
        return this;
    }

    public Material Build()
    {
        return _skyboxMaterial;
    }
}