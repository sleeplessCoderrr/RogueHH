using UnityEngine;

public class HelperUtils
{   
    private MapConfigSO _mapConfigSO;

    public HelperUtils(MapConfigSO mapConfigSO)
    {
        this._mapConfigSO = mapConfigSO;
    }
    public Vector3 GenerateRandomPosition()
    {
        var x = Random.Range(0, _mapConfigSO.widthX);
        var z = Random.Range(0, _mapConfigSO.widthY);
        return new Vector3(x, 0, z);
    }
}