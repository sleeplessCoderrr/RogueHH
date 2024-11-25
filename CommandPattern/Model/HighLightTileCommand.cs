using UnityEngine;

public class HighLightTileCommand : ICommand
{
    private GameObject _target;
    
    public HighLightTileCommand(GameObject target)
    {
        _target = target;
    }
    
    public void Execute()
    {
        var renderer = _target?.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white;
        }
        else
        {
            Debug.LogWarning("Target does not have a Renderer component.");
        }
    }
}