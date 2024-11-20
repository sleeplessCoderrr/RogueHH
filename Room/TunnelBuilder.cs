using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelBuilder : IBuild
{   
    private TunnelConfig _tunnelConfig;
    public TunnelBuilder(TunnelConfig tunnelConfig)
    {
        this._tunnelConfig = tunnelConfig;
    }

    public void Build()
    {
        
    }

    public void Reset()
    {
        
    }
}

