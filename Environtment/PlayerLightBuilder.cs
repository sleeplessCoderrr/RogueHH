using UnityEngine;

public class PlayerLightBuilder
{
    private GameObject _lightObject;
    private Light _lightComponent;

    public PlayerLightBuilder()
    {
        _lightObject = new GameObject("PlayerLight");
        _lightComponent = _lightObject.AddComponent<Light>();
    }

    public PlayerLightBuilder SetType(LightType type)
    {
        _lightComponent.type = type;
        return this;
    }

    public PlayerLightBuilder SetColor(Color color)
    {
        _lightComponent.color = color;
        return this;
    }

    public PlayerLightBuilder SetIntensity(float intensity)
    {
        _lightComponent.intensity = intensity;
        return this;
    }

    public PlayerLightBuilder SetRange(float range)
    {
        _lightComponent.range = range;
        return this;
    }

    public PlayerLightBuilder SetParent(Transform parent)
    {
        _lightObject.transform.SetParent(parent);
        return this;
    }

    public PlayerLightBuilder SetLocalPosition(Vector3 position)
    {
        _lightObject.transform.localPosition = position;
        return this;
    }

    public PlayerLightBuilder SetShadow()
    {
        _lightComponent.shadows = LightShadows.Soft; 
        _lightComponent.shadowStrength = 0.8f;     
        _lightComponent.shadowBias = 0.05f;       
        _lightComponent.shadowNormalBias = 0.4f;  
        return this;
    }

    public Light Build()
    {
        return _lightComponent;
    }
}
