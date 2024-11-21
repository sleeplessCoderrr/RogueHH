using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelBuilder : IBuild
{   
    private TunnelConfig _tunnelConfig;
    private List<Vector3> _path;
    public TunnelBuilder(TunnelConfig tunnelConfig)
    {
        this._tunnelConfig = tunnelConfig;
    }

    public void Build()
    {
        foreach (var point in _path)
        {
            GameObject.Instantiate(_tunnelConfig.floorTile, point, Quaternion.identity);
        }
    }

    public void Reset()
    {
        _path = new List<Vector3>();        
    }
}

