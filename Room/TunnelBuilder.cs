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

    public void Build(Vector3 position)
    {
        foreach (var point in _path)
        {
            GameObject.Instantiate(_tunnelConfig.floorTile, point, Quaternion.identity);
        }
    }
}

