using UnityEngine;

public class HighLightTileCommand : ICommand
{
    public void Execute(Renderer target)
    {
        var renderer = target.GetComponentInChildren<Renderer>();
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