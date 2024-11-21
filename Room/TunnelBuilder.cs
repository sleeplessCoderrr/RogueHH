using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelBuilder : IBuild
{   
    private TunnelConfigSO _tunnelConfig;
    private List<Vector3> _path;
    public TunnelBuilder(TunnelConfigSO tunnelConfig)
    {
        this._tunnelConfig = tunnelConfig;
    }

    public void Build(Vector3 position)
    {
        
    }
}

